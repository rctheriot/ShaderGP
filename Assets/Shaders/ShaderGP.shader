Shader "GeneticShader/Template"
{
	Properties {
    _MainTex ("Texture", 2D) = "white" {}
  }
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 100
		ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct appdata
			{
				float4 vertex : POSITION;
				fixed2 uv: TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			  float2 uv : TEXCOORD0;
			};

			v2f vert (appdata v)
			{
				v2f o;
        v.vertex.x += 0.0;
        v.vertex.x += 0.0;
        v.vertex.x += 0.0;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col;
				fixed4 uv = tex2D(_MainTex, i.uv);
        col.r = 1.0;
        col.g = 1.0;
        col.b = 1.0;
        col.a = 1.0;
			  return col;
			}
			ENDCG
		}
	}
}
