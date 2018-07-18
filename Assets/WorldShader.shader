Shader "Unlit/WorldShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,0)
		_CircleRadius("Spotlight size", Range(0,1)) = 0.1
		_RingSize("Ring size", Range(0,1)) = 0.1
	}
		SubShader
	{
		Tags { "IgnoreProjector" = "True"
		"Queue" = "Transparent"
		"RenderType" = "Transparent" }
		LOD 100
		

		Pass
		{

		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		Cull Back

			CGPROGRAM
			#pragma vertex vert
			//#pragma geometry geom
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2g
			{
				float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			struct g2f
			{
				float4 projectionSpaceVertex : SV_POSITION;
				float3 worldPos : TEXCOORD3;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			float4 _Color;
			float _CircleRadius;
			float _RingSize;
			uniform float4 _Collisions[264];
			uniform float _ArrayLength = 0;
			
			v2g vert (appdata v)
			{
				v2g o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}
			
			fixed4 frag (g2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = _Color;
				
				for (int k = 0; k < _ArrayLength; k++) {
					float dist = distance(i.worldPos.xy, _Collisions[k].xy);
					if (dist < _CircleRadius) {
						if (_Collisions[k].w > col.w) {
							col.w = _Collisions[k].w;
						}
					}
					else if (dist > _CircleRadius && dist < _CircleRadius + _RingSize) {
						float blendStrength = dist - _CircleRadius;
						float lerpValue = lerp(_Collisions[k].w, 0, blendStrength / _RingSize);
						if (lerpValue > col.w) {
							col.w = lerpValue;
						}
					}
				}

				return col;
			}
			ENDCG
		}
	}
}
