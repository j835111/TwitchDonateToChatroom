using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;

namespace TwitchDonateToChatroom
{
    public static class Extensions
    {
        

		static uint ToHex(char c)
		{
			ushort x = (ushort)c;
			if (x >= '0' && x <= '9')
				return (uint)(x - '0');

			x |= 0x20;
			if (x >= 'a' && x <= 'f')
				return (uint)(x - 'a' + 10);
			return 0;
		}


		static uint ToHexD(char c)
		{
			var j = ToHex(c);
			return (j << 4) | j;
		}
	}
}
