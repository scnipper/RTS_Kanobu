// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/FillAmounShader"
{
   Properties
    {
        _MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
        _Fill ("Fill", Range(0.0, 1.0)) = 1.0
        _MinY ("MinY", Float) = 0
        _MaxY ("MaxY", Float) = 1
        _FillColor ("FillColor", Color) = (1,1,1,1)
     }

    SubShader
    {
        LOD 200

        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }

        Pass
        {
            Cull Off 
            Lighting Off
            ZWrite Off
            Offset -1, -1
            Fog { Mode Off }
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _MinY;
            float _MaxY;
            float _Fill;
            half4 _FillColor;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0; 
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                fixed4 color : COLOR;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                o.color = v.color;
                return o;
            }

            half4 frag (v2f IN) : COLOR 
            {
                
                if ((IN.texcoord.y<_MinY)|| (IN.texcoord.y>(_MinY+_Fill*(_MaxY-_MinY))))
                {
                    float4 textColor = tex2D(_MainTex, IN.texcoord);
                    
                    if(textColor.a > 0.1)
                     {
                        return _FillColor;
                    } 
                    else 
                    {
                        return textColor * IN.color;
                    }
                    
    
                } else {
                    float4 textColor = tex2D(_MainTex, IN.texcoord);
                
                
                    return textColor * IN.color;
                }
                
                
                

            }
            ENDCG
        }
    }
}
