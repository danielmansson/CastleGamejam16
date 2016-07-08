Shader "Unlit/RenderToScreen"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_EdgeTex("Edge Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white" {}
		_Threshold1("Threshold1", Float) = 1
		_Threshold2("Threshold2", Range(0.1,5)) = 1
		_TimeOffset("Time offset", Range(0,5)) = 1
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		ZWrite Off           
			Blend SrcAlpha OneMinusSrcAlpha
			BlendOp Add
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _EdgeTex;
			float4 _EdgeTex_ST;
			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
			float _Threshold1;
			float _Threshold2;
			float _TimeOffset;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float t = _TimeOffset + _Time.r;
				fixed4 edge = tex2D(_EdgeTex, i.uv);
				fixed4 noise = tex2D(_NoiseTex, i.uv + float2(t, t * 1.4));
				fixed4 noise2 = tex2D(_NoiseTex, i.uv*0.15 + float2(-t*0.5, -t * 1.1));
				fixed4 noise3 = tex2D(_NoiseTex, i.uv*0.4 + float2(t*0.11, +t * 0.5));
				fixed4 col = tex2D(_MainTex, i.uv);

				float v = (1 - edge.a);

				v += (noise.r + noise2.r + noise3.r) * _Threshold2;

				if (v > _Threshold1)
					discard;
				if (v > _Threshold1 - 0.00 - (noise.r + 0.5*(1 + sin(_Time.a + i.uv.x *6))) * 0.1)
					col = float4(1 - col.r, 1 - col.g, 1 - col.b, 1);

				//col.r = col.g = col.b = col.a;

				return col;
			}
			ENDCG
		}
	}
}
