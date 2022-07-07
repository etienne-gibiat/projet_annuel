namespace _Elementis.Scripts.WFC
{
    using System;
    internal class XoRoRNG
    {

        public long State0 { get; set; }
        public long State1 { get; set; }

        private static readonly long DOUBLE_MASK = (1L << 53) - 1;
        private static readonly double NORM_53 = 1.0 / (1L << 53);
        private static readonly long FLOAT_MASK = (1L << 24) - 1;
        private static readonly double NORM_24 = 1.0 / (1L << 24);

        /// <summary>
        /// Creates a generator seeded using four calls to System.Random
        /// </summary>
        public XoRoRNG()
        {
            System.Random rand = new System.Random();
            State0 = (long)((rand.NextDouble() - 0.5) * 4503599627370496) ^ (long)(((rand.NextDouble() - 0.5) * 2.0) * -9223372036854775808);
            State1 = (long)((rand.NextDouble() - 0.5) * 4503599627370496) ^ (long)(((rand.NextDouble() - 0.5) * 2.0) * -9223372036854775808);
            if ((State0 | State1) == 0L)
                State0 = 1L;
        }

        /// <summary>
        /// Creates a generator seeded using LightRNG for both the states
        /// </summary>
        /// <param name="seed">Parameter name expalins it!!!</param>
        public XoRoRNG(long seed)
        {
            SetSeed(seed);
        }

        /// <summary>
        /// Creates a generator seeded using the two given states as parameter. The states are left unchanged unless they both are 0. If both are 0, stateA is changed to 1
        /// </summary>
        /// <param name="stateA">State0</param>
        /// <param name="stateB">State1</param>
        public XoRoRNG(long stateA, long stateB)
        {
            SetSeed(stateA, stateB);
        }

        /// <summary>
        /// Returns a Random Integer made of the given number of bits
        /// </summary>
        /// <param name="bits">The size of the number, in bits, to be returned</param>
        /// <returns>Random number of "bits" size</returns>
        public int Next(int bits)
        {
            long s0 = State0;
            long s1 = State1;
            int result = UnsignedRightShift((int)(s0 + s1), (32 - bits));
            s1 ^= s0;
            State0 = (s0 << 55 | UnsignedRightShift(s0, 9)) ^ s1 ^ (s1 << 14);
            State1 = (s1 << 36 | UnsignedRightShift(s1, 28));
            return result;
        }

        /// <summary>
        /// A Random Integer after typecasting the result from NextLong to int
        /// </summary>
        /// <returns>Obviously a Random Integer!!!</returns>
        public int NextInt()
        {
            return (int)NextLong();
        }

        /// <summary>
        /// A Random Integer between inclusive 0 and exclusive bound. The bound can be negative, which will give non-negative integer
        /// </summary>
        /// <param name="bound">Exclusive bound; can be negative</param>
        /// <returns>Random Integer between 0(inclusive) and bound(exclusive)</returns>
        public int NextInt(int bound)
        {
            return (int)((bound * UnsignedRightShift(NextLong(), 33)) >> 31);
        }

        /// <summary>
        /// A Random Integer between inclusive inner and exclusive bound.
        /// </summary>
        /// <param name="inner">Inclusive inner bound; Can be positive or negative</param>
        /// <param name="outer">Exclusive outer bound; Should be positive and greater or equal to inner bound</param>
        /// <returns>Random Integer between inner(inclusive) and outer(exclusive)</returns>
        public int NextInt(int inner, int outer)
        {
            return inner + NextInt(outer - inner);
        }

        /// <summary>
        /// Returns a Random Long
        /// </summary>
        /// <returns>A Random Long, duh!!!</returns>
        public long NextLong()
        {
            long s0 = State0;
            long s1 = State1;
            long result = s0 + s1;

            s1 ^= s0;
            State0 = (s0 << 55 | UnsignedRightShift(s0, 9)) ^ s1 ^ (s1 << 14);
            State1 = (s1 << 36 | UnsignedRightShift(s1, 28));
            return result;
        }

        /// <summary>
        /// A Random Long between inclusive 0 and exclusive bound. The bound can be negative, which will give non-negative long
        /// </summary>
        /// <param name="bound">Exclusive bound; can be negative</param>
        /// <returns>Random Long between 0(inclusive) and bound(exclusive)</returns>
        public long NextLong(long bound)
        {
            long rand = NextLong();
            long randLow = rand & 4294967295;
            long boundLow = bound & 4294967295;
            rand = UnsignedRightShift(rand, 32);
            bound >>= 32;
            long z = (randLow * boundLow >> 32);
            long t = rand * boundLow + z;
            long tLow = t & 4294967295;
            t = UnsignedRightShift(t, 32);
            return rand * bound + t + (tLow + randLow * bound >> 32) - (z >> 63) - (bound >> 63);
        }

        /// <summary>
        /// A Random Long between inclusive inner and exclusive bound.
        /// </summary>
        /// <param name="inner">Inclusive inner bound; Can be positive or negative</param>
        /// <param name="outer">Exclusive outer bound; Should be positive and greater or equal to inner bound</param>
        /// <returns></returns>
        public long NextLong(long inner, long outer)
        {
            return inner + NextLong(outer - inner);
        }

        public double NextDouble()
        {
            return (NextLong() & DOUBLE_MASK) * NORM_53;
        }

        public float NextFloat()
        {
            return (float)((NextLong() & FLOAT_MASK) * NORM_24);
        }

        public bool NextBoolean()
        {
            return NextLong() < 0L;
        }

        public void NextBytes(sbyte[] bytes)
        {
            int i = bytes.Length, n = 0;
            while (i != 0)
            {
                n = Math.Min(i, 8);
                for (long bits = NextLong(); n-- != 0; bits = UnsignedRightShift(bits, 8))
                {
                    Console.WriteLine((sbyte)bits);
                    bytes[--i] = (sbyte)bits;
                }
            }
        }

        /// <summary>
        /// Generates the two states based on the seed provided by running LightRNG's algorithm
        /// </summary>
        /// <param name="seed"></param>
        public void SetSeed(long seed)
        {
            long state = seed + -7046029254386353131,
                    z = state;
            z = (z ^ UnsignedRightShift(z, 30)) * -4658895280553007687;
            z = (z ^ UnsignedRightShift(z, 27)) * -7723592293110705685;
            State0 = z ^ UnsignedRightShift(z, 31);
            state += -7046029254386353131;
            z = state;
            z = (z ^ UnsignedRightShift(z, 30)) * -4658895280553007687;
            z = (z ^ UnsignedRightShift(z, 27)) * -7723592293110705685;
            State1 = z ^ UnsignedRightShift(z, 31);
        }

        public void SetSeed(long stateA, long stateB)
        {
            State0 = stateA;
            State1 = stateB;
            if ((stateA | stateB) == 0L)
                State0 = 1L;
        }

        /// <summary>
        /// Produces a copy of this XoRoRNG instance, such that next generated numbers by the returned instance is 
        /// same as the current instance's next generated numbers. This just copies the states to the new instance 
        /// and, usually, produce new value with the same exact states.
        /// </summary>
        /// <returns></returns>
        public XoRoRNG Copy()
        {
            XoRoRNG next = new XoRoRNG(State0);
            next.State0 = State0;
            next.State1 = State1;
            return next;
        }

        public int HashCode()
        {
            return (int)(31L * (State0 ^ UnsignedRightShift(State0, 32)) + (State1 ^ UnsignedRightShift(State1, 32)));
        }

        private long UnsignedRightShift(long value, int shiftBits)
        {
            return (long)((ulong)value >> shiftBits);
        }

        private int UnsignedRightShift(int value, int shiftBits)
        {
            return (int)((uint)value >> shiftBits);
        }
    }
}