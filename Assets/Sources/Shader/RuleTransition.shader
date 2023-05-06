//这个跟Unity默认的Sprites/Default 是一样的，只是没有批处理
Shader "Miao/RuleTransition"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_MixTex("Mix Texture", 2D) = "white" {}
		_Progress("进度条",range(0,1)) = 1
		[Toggle] _Reverse("反向",float) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off         //关闭背面剔除
			Lighting Off     //关闭灯光
			ZWrite Off       //关闭Z缓冲
			Blend One OneMinusSrcAlpha     //混合源系数one(1)  目标系数OneMinusSrcAlpha(1-one=0)

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ PIXELSNAP_ON       //告诉Unity编译不同版本的Shader,这里和后面vert中的PIXELSNAP_ON对应
				#pragma shader_feature ETC1_EXTERNAL_ALPHA
				//#pragma multi_compile _REVERSE_ON//多重编译
				#pragma shader_feature _REVERSE_ON
				#include "UnityCG.cginc"

			struct appdata_t                           //vert输入
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f                                 //vert输出数据结构
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord  : TEXCOORD0;
			};

			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif


				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			sampler2D _MixTex;
			fixed _Progress;
			float _Reverse;

			fixed4 SampleSpriteTexture(float2 uv)
			{
				fixed4 baseColor = tex2D(_MainTex, uv);
#ifdef _REVERSE_ON//定义成功了，反选是靠两个不同的纹理图片实现的，而不是shader
				fixed a = tex2D(_MixTex, uv).r;
				fixed step = 0.5 * _Progress;
				a = smoothstep(a - step, a,1 - _Progress);
				fixed4 color = fixed4(baseColor.r - a, baseColor.g - a, baseColor.b - a, baseColor.a);
#else
				//本身对黑色部分做阈值判断，直接要么0要么1
				fixed a = tex2D(_MixTex, uv).r;
				fixed step = 0.5 * _Progress;
				//fixed t = Lerp(0,1.25,_Progress);
				//_Progress = Lerp(0,1.25,_Progress);

				a = smoothstep(a - step, a, _Progress);
				fixed4 color = fixed4(baseColor.r - a, baseColor.g - a, baseColor.b - a, baseColor.a);
#endif
					
				

#if ETC1_EXTERNAL_ALPHA         //etc1没有透明通道，从另一图中取a值
					// get the color from an external texture (usecase: Alpha support for ETC1 on android)
					color.a = tex2D(_AlphaTex, uv).r;
#endif //ETC1_EXTERNAL_ALPHA
				return color;


			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
		}
}
