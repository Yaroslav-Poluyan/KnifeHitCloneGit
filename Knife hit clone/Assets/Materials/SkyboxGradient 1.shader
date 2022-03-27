// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Feelnside/SkyboxGradient"
{
    Properties
    {
        //_FogColorUp ("Top Color", Color) = (0.5, 0.5, 0.5, 1)
        //_FogColorDown ("Bottom Color", Color) = (0.15, 0.15, 0.15, 1)
        [NoScaleOffset] _NoiseTex ("Noise Texture", 2D) = "grey" {}
    }

    SubShader
    {
        Tags { "Queue"="Background" }

        Pass
        {
            ZWrite Off
            Cull Off
            Fog { Mode Off }
            CGPROGRAM
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            half3 _FogColorUp;
            half3 _FogColorDown;
            half4 screenPos;
                          
            fixed4 result;

            sampler2D _NoiseTex;
            float4 _NoiseTex_TexelSize;
            
            struct appdata
            {
                half4 vertex : POSITION;
            };
            
            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.pos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 screenPos = i.screenPos.xy / i.screenPos.w;
                
                // if using linear color space, convert the color values back to gamma space
                #if !defined(UNITY_COLORSPACE_GAMMA)
                _FogColorUp.rgb = LinearToGammaSpace(_FogColorUp);
                _FogColorDown.rgb = LinearToGammaSpace(_FogColorDown);
                #endif

                result.rgb = lerp(_FogColorDown, _FogColorUp, screenPos.y);

                // calculate pixel accurate UVs for the noise texture
                // screen pos * screen resolution * (1 / noise texture resolution)
                float2 noiseUV = screenPos * _ScreenParams.xy * _NoiseTex_TexelSize.xy;
                half3 noise = tex2D(_NoiseTex, noiseUV);
 
                // scale noise to be a range of -0.5 to 1.5, and divide by the expected 8 bit per channel color precision
                half3 dither = (noise * 2.0 - 0.5) / 255.0;
 
                // add dither to color
                result.rgb += dither;

                // convert back to linear space
                #if !defined(UNITY_COLORSPACE_GAMMA)
                result.rgb = GammaToLinearSpace(result);
                #endif
                
                return result;
            }
            ENDCG
        }
    }
}
