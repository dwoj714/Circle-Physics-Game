Shader "Unlit/TestShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex vFunction
			#pragma fFunction

			#include "UnityCG.cginc"

			struct  appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			fixed4 _Color;
			sampler2D _MainTex;

			v2f vFunction(appdata IN)
			{
				v2f OUT;

				OUT.position = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv

				return OUT;
			}

			fixed4 fFunction(v2f IN) : SV_Target
			{
				fixed4 pixelColor = tex2D(_MainTex, IN.uv) * _Color
				return pixelColor;
			}

			ENDCG
		}
	}
}
