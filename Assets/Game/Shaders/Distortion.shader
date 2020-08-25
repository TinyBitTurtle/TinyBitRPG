Shader "Custom/distortion"
{
	Properties
	{
			_CutOut("CutOut (A)", 2D) = "black" {}
			_BumpMap("Normalmap", 2D) = "bump" {}
			_BumpAmt("Distortion", Float) = 10
	}

	Category
	{
		Tags { "Queue" = "Transparent"  "IgnoreProjector" = "True"  "RenderType" = "Opaque" }
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off}

		SubShader 
		{
			GrabPass 
			{
				"_GrabTexture"
			}

			Pass 
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma multi_compile_particles
				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					float2 texcoord: TEXCOORD0;
				};

				struct v2f 
				{
					float4 vertex : POSITION;
					float4 uvgrab : TEXCOORD0;
					float2 uvbump : TEXCOORD1;
					float2 uvcutout : TEXCOORD2;
				};

				sampler2D _BumpMap,_CutOut,_GrabTexture;
				float _BumpAmt;
				float4 _GrabTexture_TexelSize;
				float4 _BumpMap_ST,_CutOut_ST;

				v2f vert(appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*-1) + o.vertex.w) * 0.5;
					o.uvgrab.zw = o.vertex.zw;
					o.uvbump = TRANSFORM_TEX(v.texcoord, _BumpMap);
					o.uvcutout = TRANSFORM_TEX(v.texcoord, _CutOut);
					return o;
				}

				half4 frag(v2f i) : COLOR
				{
					half2 bump = UnpackNormal(tex2D(_BumpMap, i.uvbump)).rg;
					float2 offset = bump * _BumpAmt * _GrabTexture_TexelSize.xy;
					i.uvgrab.xy = offset * i.uvgrab.z + i.uvgrab.xy;

					half4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uvgrab));
					fixed4 cut = tex2D(_CutOut, i.uvcutout);
					fixed4 emission = col;
					emission.a = (cut.a);
					return emission;
				}
				ENDCG
			}
		}
	}
}