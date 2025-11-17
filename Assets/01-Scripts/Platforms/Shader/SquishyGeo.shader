Shader "Custom/Lotts/Wobbles/SquishyGeo"
{
    Properties
    {
        [Position] _DisplacePos("Displace Pos", Vector) = (1,2,3)
        [Radius] _Radius("Radius", Float) = 0.1
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        [MainTexture] _BaseMap("Base Map", 2D) = "white"
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            uniform float _Radius;
            uniform float4 _DisplacePos; 

            struct vertIn
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2g
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            struct g2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                float4 _BaseMap_ST;
            CBUFFER_END
            
            v2g vert(vertIn input)
            {
                v2g output;
                output.vertex = input.vertex;
                output.uv = TRANSFORM_TEX(input.uv, _BaseMap);

                return output;
            }

            [maxvertexcount(3)]
            void geom(triangle v2g input[3],
                      inout TriangleStream<g2f> triStream)
            {
                g2f o;
                for (int i = 0; i < 3; i ++) {
                    //in range
                    float3 w = TransformObjectToWorld(input[i].vertex.xyz);
                    if (distance(w, _DisplacePos) < _Radius) {
                        input[i].vertex.y = TransformWorldToObject(_DisplacePos).y;
                    }
                    
                    o.vertex = TransformObjectToHClip(input[i].vertex);
                    o.uv = input[i].uv;
                    
                    triStream.Append(o);
                }
                triStream.RestartStrip();
            }

            half4 frag(g2f input) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv) * _BaseColor;
                return color;
            }
            ENDHLSL
        }
    }
}
