Shader "Multiply"
{
	Properties
	{
		Color_D9BCE816("BaseColor", Color) = (0.06074226,0.6132076,0.1164393,0)
		[NoScaleOffset] Texture2D_98DB4766("Texture2D", 2D) = "white" {}
	}

		HLSLINCLUDE
#define USE_LEGACY_UNITY_MATRIX_VARIABLES
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Packing.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/EntityLighting.hlsl"
#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariables.hlsl"
#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"
#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl"

		CBUFFER_START(UnityPerMaterial)
		float4 Color_D9BCE816;
	CBUFFER_END

		float2 Vector2_7E91FF14;
	TEXTURE2D(Texture2D_98DB4766); SAMPLER(samplerTexture2D_98DB4766); float4 Texture2D_98DB4766_TexelSize;
	struct SurfaceDescriptionInputs
	{
	};


	void Unity_Multiply_float(float A, float B, out float Out)
	{
		Out = A * B;
	}

	struct SurfaceDescription
	{
		float Out;
	};

	SurfaceDescription PopulateSurfaceData(SurfaceDescriptionInputs IN)
	{
		SurfaceDescription surface = (SurfaceDescription)0;
		float _Multiply_4851CAC5_Out;
		Unity_Multiply_float(_Time.y, 0.25, _Multiply_4851CAC5_Out);

		surface.Out = _Multiply_4851CAC5_Out;
		return surface;
	}

	struct GraphVertexInput
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
		float4 tangent : TANGENT;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	GraphVertexInput PopulateVertexData(GraphVertexInput v)
	{
		return v;
	}

	ENDHLSL

		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			//#pragma fragment frag

			struct GraphVertexOutput
			{
				float4 position : POSITION;

			};

			GraphVertexOutput vert(GraphVertexInput v)
			{
				v = PopulateVertexData(v);

				GraphVertexOutput o;
				float3 positionWS = TransformObjectToWorld(v.vertex);
				o.position = TransformWorldToHClip(positionWS);

				return o;
			}

			//float4 frag(GraphVertexOutput IN) : SV_Target
			//{

			//	SurfaceDescriptionInputs surfaceInput = (SurfaceDescriptionInputs)0;

			//	SurfaceDescription surf = PopulateSurfaceData(surfaceInput);
			//	return half4(surf._Multiply_4851CAC5_Out, surf._Multiply_4851CAC5_Out, surf._Multiply_4851CAC5_Out, 1.0);

			//}
			ENDHLSL
		}
	}
}
