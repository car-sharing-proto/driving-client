Shader"Custom/Brending" {
    Properties {
        _Background ("Background", Color) = (1.0, 1.0, 1.0, 1.0)
        _MainTex ("Texture", 2D) = "white" {}
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.5
    }

    SubShader {
        Tags { "RenderType" = "Opaque" }
        CGPROGRAM
        #pragma surface surf Standard

        struct Input
        {
            float2 uv_MainTex;
        };
            
        sampler2D _MainTex;
        float4 _Background;
        float _Smoothness;
        float _Metallic;
    
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float4 col = tex2D(_MainTex, IN.uv_MainTex);
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;             
            o.Albedo = lerp(_Background.rgb, col.rgb, col.a);
            
        }
        ENDCG
    } 
    Fallback"Diffuse"
}