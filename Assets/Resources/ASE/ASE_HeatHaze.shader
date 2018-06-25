// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ASE/HeatHaze"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_Haze_mask("Haze_mask", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
		_HazeNormalIntensity("HazeNormalIntensity", Range( 0 , 1)) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	Category 
	{
		SubShader
		{
			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off
			Lighting Off 
			ZWrite Off
			GrabPass{ }

			Pass {
			
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 3.0
				#pragma multi_compile_particles
				#pragma multi_compile_fog
				#include "UnityShaderVariables.cginc"


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					
				};

				struct v2f 
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
					#endif
					UNITY_VERTEX_OUTPUT_STEREO
					float4 ase_texcoord3 : TEXCOORD3;
				};
				
				uniform sampler2D _MainTex;
				uniform fixed4 _TintColor;
				uniform float4 _MainTex_ST;
				uniform sampler2D_float _CameraDepthTexture;
				uniform float _InvFade;
				uniform sampler2D _GrabTexture;
				uniform sampler2D _Normal;
				uniform float _HazeNormalIntensity;
				uniform sampler2D _Haze_mask;
				uniform float4 _Haze_mask_ST;
				inline float4 ASE_ComputeGrabScreenPos( float4 pos )
				{
					#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
					#else
					float scale = 1.0;
					#endif
					float4 o = pos;
					o.y = pos.w * 0.5f;
					o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
					return o;
				}
				

				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
					float4 screenPos = ComputeScreenPos(ase_clipPos);
					o.ase_texcoord3 = screenPos;
					

					v.vertex.xyz +=  float3( 0, 0, 0 ) ;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#ifdef SOFTPARTICLES_ON
						o.projPos = ComputeScreenPos (o.vertex);
						COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					o.color = v.color;
					o.texcoord = v.texcoord;
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag ( v2f i  ) : SV_Target
				{
					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate (_InvFade * (sceneZ-partZ));
						i.color.a *= fade;
					#endif

					float4 screenPos = i.ase_texcoord3;
					float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( screenPos );
					float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
					float2 appendResult17 = (float2(_SinTime.z , _CosTime.z));
					float2 uv16 = i.texcoord.xy * float2( 1,1 ) + appendResult17;
					float4 screenColor1 = tex2D( _GrabTexture, ( ase_grabScreenPosNorm + ( tex2D( _Normal, uv16 ) * _HazeNormalIntensity ) ).xy );
					float2 uv_Haze_mask = i.texcoord.xy * _Haze_mask_ST.xy + _Haze_mask_ST.zw;
					float4 appendResult10 = (float4(( ( i.color * screenColor1 ) * _TintColor.r ).rgb , tex2D( _Haze_mask, uv_Haze_mask ).r));
					

					fixed4 col = appendResult10;
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
259;395;1562;453;2194.064;-83.66377;1.317112;True;True
Node;AmplifyShaderEditor.SinTimeNode;18;-2184.665,265.6141;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CosTime;19;-2104.665,446.6141;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;17;-1937.665,275.6141;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-1702.665,274.6141;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;14;-1182.288,534.3499;Float;False;Property;_HazeNormalIntensity;HazeNormalIntensity;2;0;Create;True;0;0;False;0;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;12;-1349.792,279.6036;Float;True;Property;_Normal;Normal;1;0;Create;True;0;0;False;0;None;e9742c575b8f4644fb9379e7347ff62e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-923.7634,293.5724;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GrabScreenPosition;2;-1293.494,-16.39216;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-876.5341,75.34641;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.VertexColorNode;5;-768.8799,-361.7536;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;1;-681.5,-52.16216;Float;False;Global;_GrabScreen0;Grab Screen 0;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-492.5,-100.5;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;3;-555.3191,117.213;Float;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-248.7346,15.15521;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;8;-598.8851,309.244;Float;True;Property;_Haze_mask;Haze_mask;0;0;Create;True;0;0;False;0;None;8c4a7fca2884fab419769ccc0355c0c1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;10;-54.164,87.58252;Float;False;COLOR;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;357.538,-49.65723;Float;False;True;2;Float;ASEMaterialInspector;0;5;ASE/HeatHaze;0b6a9f8b4f707c74ca64c0be8e590de0;0;0;SubShader 0 Pass 0;2;True;2;SrcAlpha;OneMinusSrcAlpha;0;One;Zero;False;True;Off;True;True;True;True;False;False;True;2;False;False;True;4;Queue=Transparent;IgnoreProjector=True;RenderType=Transparent;PreviewType=Plane;False;0;0;0;False;False;False;False;False;False;False;False;False;True;2;0;0;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;17;0;18;3
WireConnection;17;1;19;3
WireConnection;16;1;17;0
WireConnection;12;1;16;0
WireConnection;15;0;12;0
WireConnection;15;1;14;0
WireConnection;13;0;2;0
WireConnection;13;1;15;0
WireConnection;1;0;13;0
WireConnection;6;0;5;0
WireConnection;6;1;1;0
WireConnection;4;0;6;0
WireConnection;4;1;3;1
WireConnection;10;0;4;0
WireConnection;10;3;8;1
WireConnection;0;0;10;0
ASEEND*/
//CHKSM=C6C8DC324C9CC0078EBC3912B539638D3A84F57F