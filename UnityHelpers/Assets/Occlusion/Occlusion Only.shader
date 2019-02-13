Shader "Custom/Occlusion only"
{
    SubShader
    {
        Tags { "Queue"="Geometry-1" "RenderType"="Opaque" "IgnoreProjector"="True" }
        LOD 100
        Cull Back
        ZWrite On
        ColorMask 0

        Pass
        {            
            CGPROGRAM
            
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
        
            #include "UnityCG.cginc"

            #pragma multi_compile_instancing
                
            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                
                return half4(0,0,0,0);
            }
            ENDCG
        }
    }
}
