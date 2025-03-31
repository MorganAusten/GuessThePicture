Shader "Unlit/SpriteRenderer"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScrollSpeed ("Scroll Speed", Vector) = (0.1,0,0,0)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {

              ZWrite Off // Désactive l'écriture dans le Z-buffer
              ZTest Always // Ignore le Z-test, ce qui empêche les objets de disparaître
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
            float4 _ScrollSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv  + _ScrollSpeed.xy * _Time.y;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}

// Shader "Custom/MyShader"
// {
//     Properties
//     {
//         _MainTex ("Base Texture", 2D) = "white" {}
//     }
//     SubShader
//     {
//         Pass
//         {
//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
//             #include "UnityCG.cginc"

//             struct appdata
//             {
//                 float4 vertex : POSITION;
//                 float2 uv : TEXCOORD0;
//             };

//             struct v2f
//             {
//                 float2 uv : TEXCOORD0;
//                 float4 vertex : SV_POSITION;
//             };

//             sampler2D _MainTex;

//             v2f vert(appdata v)
//             {
//                 v2f o;
//                 o.vertex = UnityObjectToClipPos(v.vertex);
//                 o.uv = v.uv;
//                 return o;
//             }

//             half4 frag(v2f i) : SV_Target
//             {
//                 return tex2D(_MainTex, i.uv);
//             }
//             ENDCG
//         }
//     }
// }

