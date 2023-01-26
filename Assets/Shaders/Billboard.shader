/*Please do support www.bitshiftprogrammer.com by joining the facebook page : fb.com/BitshiftProgrammer
Legal Stuff:
This code is free to use no restrictions but attribution would be appreciated.
Any damage caused either partly or completly due to usage this stuff is not my responsibility*/
Shader "BitshiftProgrammer/Billboard"
{
    Properties
    {
        _MainTex("Texture Image", 2D) = "white" {}
        _Scaling("Scaling", Float) = 1.0
        [Toggle] _KeepConstantScaling("Keep Constant Scaling", Int) = 1
        [Enum(RenderOnTop, 0,RenderWithTest, 4)] _ZTest("Render on top", Int) = 1
    }
        SubShader
        {
            Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "DisableBatching" = "True" }

            ZWrite On
            ZTest[_ZTest]
            Blend SrcAlpha OneMinusSrcAlpha
            Pass
            {
            CGPROGRAM

             #pragma vertex vert
             #pragma fragment frag

             uniform sampler2D _MainTex;
             int _KeepConstantScaling;
             float _Scaling;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
             };

             struct v2f
             {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
             };

             v2f vert(appdata v)
             {
                v2f o;
                float relativeScaler = (_KeepConstantScaling) ? distance(mul(unity_ObjectToWorld, v.vertex), _WorldSpaceCameraPos) : 1;
                o.vertex = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0)) + float4(v.vertex.x, v.vertex.y, 0.0, 0.0) * relativeScaler * _Scaling);
                o.uv = v.uv;
                o.color = v.color;
                return o;
             }

             float4 frag(v2f i) : SV_Target
             {
                return tex2D(_MainTex, float2(i.uv))*i.color;
             }

             ENDCG
          }
        }
}