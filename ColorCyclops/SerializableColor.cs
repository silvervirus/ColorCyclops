using System;
using UnityEngine;

namespace ColorCyclops
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public class SerializableColor
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002D70 File Offset: 0x00000F70
		public SerializableColor(Color c)
		{
			this.r = c.r;
			this.g = c.g;
			this.b = c.b;
			this.a = c.a;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public static implicit operator SerializableColor(Color c)
		{
			return new SerializableColor(c);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002DFC File Offset: 0x00000FFC
		public Color ToColor(bool value)
		{
			Color result;
			if (value)
			{
				result = new Color32(Convert.ToByte(rValue), Convert.ToByte(gValue), Convert.ToByte(bValue), 1);
			}
			else
			{
				result = new Color(this.r, this.g, this.b, 1f);
			}
			return result;
		}

		// Token: 0x04000009 RID: 9
		public float r = 0.016f;

		// Token: 0x0400000A RID: 10
		public float g = 1f;

		// Token: 0x0400000B RID: 11
		public float b = 1f;

		// Token: 0x0400000C RID: 12
		public float a = 1f;
		public float rValue;
		public float gValue;
		public float bValue;
	}
}
