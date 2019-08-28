Shader "PostEffectShader/PostEffectShader_greenCameraOverlay"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ColorDamp ("Color Damp", Float) = 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _ColorDamp;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 underlayingTexture = tex2D(_MainTex, i.uv);

				underlayingTexture.r -= _ColorDamp;
				underlayingTexture.b -= _ColorDamp;

				return underlayingTexture;
			}

			ENDCG
		}
	}
}
