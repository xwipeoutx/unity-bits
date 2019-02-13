Shader "Custom/Sweep Reveal"
{
	Properties
	{
		_Color ("Albedo", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		
		_SweepTex ("Sweep Texture", 2D) = "white" {}
		_SweepValue("Sweep value", Range(0,1)) = 1.0
		
		_EdgeWidth ("Edge Width", Range(1, 100)) = 25
		_EdgeColor ("Albedo", Color) = (0,1,0,1)
	}

	SubShader
	{
		Pass
		{
			Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
			
			Blend SrcAlpha OneMinusSrcAlpha
			
			LOD 200

			CGPROGRAM

			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

            #pragma multi_compile_instancing

			fixed4 _Color;
			sampler2D _MainTex;
			fixed4 _MainTex_ST;
			
			sampler2D _SweepTex;
			float _SweepValue;
			
			fixed _EdgeWidth;
			fixed4 _EdgeColor;

			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			    UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f 
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
				
			    UNITY_VERTEX_OUTPUT_STEREO
			};
			
			v2f vert(appdata_t v)
			{
				v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 sweepValue = tex2D (_SweepTex, i.uv);
                fixed distance = 1 - saturate((sweepValue - _SweepValue) * (500 / _EdgeWidth));
                clip(distance - 0.1);
				
				fixed4 textureAlbedo = tex2D(_MainTex, i.uv);
				fixed4 albedo = textureAlbedo * _Color;
				
				fixed4 output = albedo;
				
                output.rgb = lerp(_EdgeColor, output.rgb, distance);
				return output;
			}
			
			ENDCG
		}
	}
	
	FallBack "VertexLit"
}
