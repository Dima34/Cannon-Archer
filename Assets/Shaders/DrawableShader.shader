Shader "Custom/DrawableShader"
{

    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _PaintMap("PaintMap", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque" "LightMode" = "ForwardBase"
        }

        Pass
        {
            Lighting Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
            };

            struct appdata
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };

            sampler2D _PaintMap;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv0 = TRANSFORM_TEX(v.texcoord, _MainTex);

                o.uv1 = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;

                return o;
            }

            half4 frag(v2f o) : COLOR
            {
                half4 main_color = tex2D(_MainTex, o.uv0);
                half4 paint = (tex2D(_PaintMap, o.uv1));
                main_color *= paint;
                return main_color;
            }
            ENDCG
        }
    }
}