Shader "Custom/ColorHighlight"
{
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Threshold ("Color Match Threshold", Range(0, 1)) = 0.1
        _TargetCount ("Target Count", Int) = 1
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            sampler2D _MainTex;
            float _Threshold;
            int _TargetCount;
            float4 _TargetPositions[10]; // Posições dos alvos
            float _TargetRadii[10]; // Raio de cada alvo
            float4 _HighlightColors[10]; // Cores de destaque para cada alvo

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float3 ConvertToGrayscale(float3 color) {
                float gray = 0.299 * color.r + 0.587 * color.g + 0.114 * color.b;
                return float3(gray, gray, gray);
            }

            float ColorDistance(float3 c1, float3 c2) {
                return length(c1 - c2);
            }

            fixed4 frag(v2f i) : SV_Target {
                float4 texColor = tex2D(_MainTex, i.uv);
                float3 finalColor = ConvertToGrayscale(texColor.rgb); // Começa como cinza
                bool isInsideHighlightRadius = false;
                bool keepOriginalColor = false;

                // Verifica se o pixel está dentro de algum raio
                for (int j = 0; j < _TargetCount; j++) {
                    float distanceToTarget = distance(i.uv, _TargetPositions[j].xy);
                    if (distanceToTarget < _TargetRadii[j]) {
                        isInsideHighlightRadius = true;

                        // Se estiver dentro do raio, verifica a cor
                        if (ColorDistance(texColor.rgb, _HighlightColors[j].rgb) < _Threshold) {
                            keepOriginalColor = true; // A cor está dentro do Threshold
                        }

                        break; // O pixel já está dentro de um raio, podemos sair do loop
                    }
                }

                // Se estiver dentro de um raio e a cor for similar à cor do alvo, mantém a cor original
                if (isInsideHighlightRadius && keepOriginalColor) {
                    finalColor = texColor.rgb;
                }

                return float4(finalColor, texColor.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
