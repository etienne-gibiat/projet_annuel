﻿//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;

	public class SWShaderCreaterUI :SWShaderCreaterBase {
		public SWShaderCreaterUI(SWWindowMain _window):base(_window)
		{
			IsTextureSampleAdd = true;
			IsSprite = true;
		}
		protected override void Functions ()
		{
			base.Functions ();
		}



		protected override void PropertyField ()
		{
			base.PropertyField ();

			StringAddLine("\t\t_StencilComp (\"Stencil Comparison\", Float) = 8\n\t\t_Stencil (\"Stencil ID\", Float) = 0\n\t\t_StencilOp (\"Stencil Operation\", Float) = 0\n\t\t_StencilWriteMask (\"Stencil Write Mask\", Float) = 255\n\t\t_StencilReadMask (\"Stencil Read Mask\", Float) = 255\n\n\t\t_ColorMask (\"Color Mask\", Float) = 15\n\n\t\t[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip (\"Use Alpha Clip\", Float) = 0");
		} 
		protected override void PropertyDeclare ()
		{
			base.PropertyDeclare ();

			StringAddLine ("\t\t\tfixed4 _TextureSampleAdd;");
			StringAddLine ("\t\t\tfloat4 _ClipRect;");
		}



		protected override void Includes ()
		{
			base.Includes ();
			StringAddLine ("\t\t\t#include \"UnityUI.cginc\"");
		}
		protected override void Struct_Appdata ()
		{
			base.Struct_Appdata ();
		}
		protected override void Struct_v2f ()
		{
			base.Struct_v2f ();
			StringAddV2d ("float4", "rect_Sprite", "COLOR1");
			StringAddV2d ("float4", "worldPosition", "COLOR2");
		}
		protected override void Pragma ()
		{
			base.Pragma ();
			StringAddLine ("\t\t\t\t#pragma multi_compile __ UNITY_UI_ALPHACLIP");
		}
		protected override void Tags ()
		{
			base.Tags ();
			StringAddLine ("\t\t\t\"IgnoreProjector\"=\"True\" ");
			StringAddLine ("\t\t\t\"PreviewType\"=\"Plane\"");
			StringAddLine ("\t\t\t\"CanUseSpriteAtlas\"=\"True\"");
		}
		protected override void Ops ()
		{
			StringAddLine ("\t\t\tCull Off");
			StringAddLine ("\t\t\tLighting Off");
			StringAddLine ("\t\t\tZWrite Off");

			StringAddLine ("\t\t\tStencil {\n\t\t\t\tRef [_Stencil]\n\t\t\t\tComp [_StencilComp]\n\t\t\t\tPass [_StencilOp] \n\t\t\t\tReadMask [_StencilReadMask]\n\t\t\t\tWriteMask [_StencilWriteMask]\n\t\t\t}\n\t\t\tZTest [unity_GUIZTestMode]\n\t\t\tColorMask [_ColorMask]");
		}

		protected void Rect_Sprite()
		{
			StringAddLine(string.Format( "\t\t\t\tOUT.rect_Sprite = float4({0},{1},{2},{3});"
				,window.data.spriteRect.x.ToStringEX()
                , window.data.spriteRect.y.ToStringEX()
                , window.data.spriteRect.width.ToStringEX()
                , window.data.spriteRect.height.ToStringEX()
            ));
		}

		protected override void Vert ()
		{
			Rect_Sprite ();
			StringAddLine ("\t\t\t\tOUT.worldPosition = IN.vertex;");

			StringAddLine( "\t\t\t\tOUT.pos = mul(UNITY_MATRIX_MVP,IN.vertex);   ");
			StringAddLine ("\t\t\t\t#ifdef UNITY_HALF_TEXEL_OFFSET\n\t\t\t\tOUT.pos.xy += (_ScreenParams.zw-1.0)*float2(-1,1);\n\t\t\t\t#endif");
			StringAddLine ("\t\t\t\tOUT.color = IN.color * _Color;");
			StringAddLine ("\t\t\t\tOUT._uv_MainTex = TRANSFORM_TEX(IN.texcoord,_MainTex);");
			Screen_Vert ();
			Vert_UV_STD ();
		}
		protected override void Vert_UV_STD ()
		{
			StringAddLine ("\t\t\t\tOUT._uv_STD = float2((IN.texcoord.x - OUT.rect_Sprite.x)/OUT.rect_Sprite.z,(IN.texcoord.y - OUT.rect_Sprite.y)/OUT.rect_Sprite.w);");
			StringAddLine ("\t\t\t\tOUT._uv_STD = TRANSFORM_TEX(OUT._uv_STD,_MainTex);  ");
		}


		protected override void Frag ()
		{
			base.Frag ();
		}


		public override void ProcessExtra (SWNodeBase root)
		{
			base.ProcessExtra (root);
			StringAddLine( "\t\t\t\tresult.a *= UnityGet2DClipping(i.worldPosition.xy, _ClipRect);");
			StringAddLine( "\t\t\t\t#ifdef UNITY_UI_ALPHACLIP\n\t\t\t\tclip (result.a - 0.001);\n\t\t\t\t#endif");
		}
	}
}