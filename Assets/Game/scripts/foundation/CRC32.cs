using System;

public static class CRC32
{
    static uint[] table;
    static bool initialized = false;

    public static uint Compute(String input)
    {
        if (!initialized)
            Init();

        uint crc = 0xffffffff;
        for (int i = 0; i < input.Length; ++i)
        {
            byte index = (byte)(((crc) & 0xff) ^ input[i]);
            crc = (uint)((crc >> 8) ^ table[index]);
        }
        return ~crc;
    }

    public static void Init()
    {
        initialized = true;

        uint poly = 0xedb88320;
        table = new uint[256];
        uint value = 0;
        for (uint i = 0; i < table.Length; ++i)
        {
            value = i;
            for (int j = 8; j > 0; --j)
            {
                if ((value & 1) == 1)
                {
                    value = (uint)((value >> 1) ^ poly);
                }
                else
                {
                    value >>= 1;
                }
            }

            table[i] = value;
        }
    }
}
