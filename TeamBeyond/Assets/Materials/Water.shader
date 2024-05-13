Shader "Custom/Water" {

    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _FoamColor ("Foam Color", Color) = (1,1,1,1)

        [Header(Reflections)]
        _Reflectivity ("Reflectivity", Range(0, 1)) = 0.5
        _FresnelPower ("Fresnel Power", Range(1, 5)) = 5
        [KeywordEnum(One, Two, Three, Four)]
            _PRID ("Planar Refl. ID", Float) = 0

    }

    SubShader {
        Tags {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite On
        LOD 200

        CGPROGRAM
    
        #pragma surface surf Standard fullforwardshadows alpha:add
        #pragma target 3.0

        // To enable planar reflections, enable _PRID_ONE, if the probe's
        // targeting ID 1, _PRID_TWO if it's targeting ID 2 and so on. I'm
        // using a multicompile with a KeywordEnum to make it flexible.
        #pragma multi_compile _PRID_ONE _PRID_TWO _PRID_THREE _PRID_FOUR
        #include "PlanarReflections.cginc"

        struct Input {
            float4 screenPos;
            float3 viewDir;
        };

        fixed4 _Color;
        half _Reflectivity;
        half _FresnelPower;

        sampler2D _CameraDepthTexture;
        float4 _CameraDepthTexture_TexelSize;

        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o) {

            // We're not using these for still waters.
            o.Metallic = 0;
            o.Smoothness = 0;
            o.Normal = float3(0, 0, 1);

            half refl = _Reflectivity;
            half cos = saturate(dot(o.Normal, normalize(IN.viewDir)));
            refl += (1 - _Reflectivity) * pow(1 - cos, _FresnelPower);

            o.Emission = SamplePlanarReflections(IN.screenPos) * refl;
        }
        ENDCG
    }
    FallBack "Diffuse"
}