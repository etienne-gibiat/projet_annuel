#ifndef INCLUDE_DIGITRENDERER
#define INCLUDE_DIGITRENDERER


    #define DIGIT_NEG 10
    #define DIGIT_DOT 11
    #define DIGIT_COMMA 12
    #define DATA float4 data[15]

    uniform float Bias;
    uniform uint DisplayedData, Channel;

    uniform sampler2D Tex;
    uniform float4 DigitColor;
    uniform float Separation, Scale;
    uniform uint Precision;

    uniform float4 WireframeColor;
    uniform float WireframeSmoothing, WireframeThickness;

    static const float2 offsets[4] =
    {
        float2(-0.25,-0.5),
        float2(-0.25,0.5),
        float2(0.25,-0.5),
        float2(0.25,0.5)
    };

    static const float2 digitUvs[4] =
    {
        float2(0, 0),
        float2(0, 1),
        float2(1, 0),
        float2(1, 1)
    }; 

    static const float2 baryCoords[3] =
    {
        float2(1, 0),
        float2(0, 1),
        float2(0, 0),
    };             

    float4 GetValue(DATA)
    {
        float4 value = data[DisplayedData];
        #ifdef ABSVAL
            value = abs(value);
        #endif

        value *= Bias;

        if(Channel == 0) return value;
        return value[Channel-1];                
    }

    void AddDigitQuad(int value, float4 position, inout float count, inout TriangleStream<varyings> triStream)
    {
        varyings o;
        float localScale = Scale * 0.1;
        float digitWidth = ((1.0/832.0) * 64);
        float sideOffset = count * 0.5;
        count++;

        for(int i = 0; i < 4; i++)
        {
            float4 pos = position;
            pos.xyz = mul(UNITY_MATRIX_V, mul(unity_ObjectToWorld, float4(pos.xyz, 1.0))).xyz;
            float camScaling = min(localScale * abs(pos.z), localScale * 3);

            pos.xy += offsets[i] * camScaling;
            pos.x -= sideOffset * camScaling;
            pos.z += 0.01;

            o.vertex = mul(UNITY_MATRIX_P, pos);
            o.baryCoords = 1;

            o.data.zw = 1;
            o.data.xy = digitUvs[i];
            o.data.x = o.data.x * digitWidth;
            o.data.x += digitWidth * (value);

            triStream.Append(o);
        }                   

        triStream.RestartStrip();
    }

    void AddDigits(uint work, float4 origin, inout uint count, inout TriangleStream<varyings> triStream)
    {
        while (work > 0)
        {
            int digit = work % 10;
            work = work / 10;
            AddDigitQuad(digit, origin, count, triStream);
        }  
    }

    void DisplayNumber(float number, float4 origin, float precision,  inout uint count, inout TriangleStream<varyings> triStream)
    {
        uint work = 0;

        #ifdef DO_ROUND
            float rounder = 1 / pow(10, precision);
            number = (round(number / rounder)) * rounder;
        #endif                    

        // fractional part
        float fracPart = frac(abs(number));

        for (int i = precision - 1; i > 0; i--)
        {
            float power = pow(10, i);
            float digit = (fracPart * power) % 10;
            AddDigitQuad(digit, origin, count, triStream);
        }
        if(precision > 1) AddDigitQuad(DIGIT_DOT, origin, count, triStream);

        // whole part
        float decimalPart = floor(abs(number));
        work = decimalPart;
        if(work == 0)
        {
            AddDigitQuad(0, origin, count, triStream);
        }
        else 
        {
            AddDigits(work, origin, count, triStream);
        }
        
        // negative sign
        if(0.00 > number)
        {
            AddDigitQuad(DIGIT_NEG, origin, count, triStream);
        }
    }

    void DisplayVector(float4 number, float4 origin, inout TriangleStream<varyings> triStream)
    {
        uint count = 0;
        float precisionCap = min(Precision, 4);

        // DisplayNumber(number.w, origin, precisionCap, count, triStream);
        // AddDigitQuad(DIGIT_COMMA, origin, count, triStream);

        DisplayNumber(number.z, origin, precisionCap, count, triStream);
        AddDigitQuad(DIGIT_COMMA, origin, count, triStream);

        DisplayNumber(number.y, origin, precisionCap, count, triStream);
        AddDigitQuad(DIGIT_COMMA, origin, count, triStream);

        DisplayNumber(number.x, origin, precisionCap, count, triStream);
    }

    float GetWireframe(varyings i)
    {
        float3 barys;
        barys.xy = i.baryCoords;
        barys.z = 1 - barys.x - barys.y;

        float3 deltas = fwidth(barys);
        float3 smoothing = deltas * WireframeSmoothing;
        float3 thickness = deltas * WireframeThickness;
        barys = smoothstep(thickness, thickness + smoothing, barys);

        float minBary = min(barys.x, min(barys.y, barys.z));
        return minBary;
    }

    varyings vert (appdata v)
    {
        DATA;
        data[0]  = v.color;
        data[1]  = v.normal;
        data[2]  = v.tangent;
        data[3]  = v.uv0;
        data[4]  = v.uv1;
        data[5]  = v.uv2;
        data[6]  = v.uv3;
        data[7]  = v.uv4;
        data[8]  = v.uv5;
        data[9]  = v.uv6;
        data[10] = v.uv7;
        data[11] = v.id;
        data[12] = mul(UNITY_MATRIX_M, v.vertex);
        data[13] = v.vertex;
        data[14] = GetCustomData(v);

        varyings o;
        o.vertex = v.vertex;
        o.data = GetValue(data);
        o.baryCoords = 0;
        return o;
    }

    [maxvertexcount(102)][instance(4)]
    void geom(uint InstanceID : SV_GSInstanceID, triangle varyings input[3], inout TriangleStream<varyings> triStream)
    {
        varyings o;
        
        // base vertices
        if(InstanceID == 0)
        {
            for(int i = 0; i < 3; i++)
            {
                o.vertex = UnityObjectToClipPos(input[i].vertex);
                o.data = input[i].data;
                o.data.w = 0;
                o.baryCoords = baryCoords[i];
                triStream.Append(o);
            }
            triStream.RestartStrip();
        }
        // digit quads
        #ifdef DIGITS
        else 
        {
            float4 center = (input[0].vertex + input[1].vertex + input[2].vertex) / 3;

            int k = InstanceID - 1;

            float4 value = input[k].data;
            float4 origin = lerp(input[k].vertex, center, Separation);
            uint count  = 0;

            if(Channel != 0)
            {
                DisplayNumber(value.x, origin, Precision, count, triStream);
            }
            else 
            {
                DisplayVector(value, origin, triStream);
            }
        }
        #endif
    }

    float4 frag (varyings i) : SV_Target
    {
        float4 number = tex2D(Tex, i.data.xy) * DigitColor;
        float clipAlpha = lerp(1, number.a, i.data.w);
        clip(clipAlpha - 0.1);

        float alpha = i.data.w;

        #ifdef WIREFRAME
            float wireframe = GetWireframe(i);
            float4 rgb = lerp(WireframeColor, i.data, wireframe);
            alpha = max(i.data.w, step(wireframe, 0));
        #else
            float4 rgb = i.data;
        #endif

        float4 final = lerp(rgb, number, i.data.w);
        final.a = alpha;

        return final;
    }

#endif // ifndef INCLUDE_DIGITRENDERER