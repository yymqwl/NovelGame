// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ASE/Shakewave"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_Distortion("Distortion", 2D) = "bump" {}
		_Mask("Mask", 2D) = "white" {}
		_distotfact("distotfact", Vector) = (0,0,0,0)
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
				uniform sampler2D _Distortion;
				uniform float4 _Distortion_ST;
				uniform float2 _distotfact;
				uniform sampler2D _Mask;
				uniform float4 _Mask_ST;
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
					float4 uv260 = ase_grabScreenPosNorm;
					float2 uv_Distortion = i.texcoord.xy * _Distortion_ST.xy + _Distortion_ST.zw;
					float4 screenColor4 = tex2D( _GrabTexture, ( uv260 + float4( ( ( (UnpackNormal( tex2D( _Distortion, uv_Distortion ) )).xy * _distotfact ) * _SinTime.w ), 0.0 , 0.0 ) ).xy );
					float2 uv_Mask = i.texcoord.xy * _Mask_ST.xy + _Mask_ST.zw;
					float4 appendResult67 = (float4(screenColor4.rgb , tex2D( _Mask, uv_Mask ).a));
					

					fixed4 col = appendResult67;
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
-184;537;1562;570;2453.502;298.2023;1.092649;True;True
Node;AmplifyShaderEditor.CommentaryNode;21;-2304.025,-520.6072;Float;False;1457.445;616.1181;Comment;14;30;39;4;58;59;60;61;62;63;64;67;68;69;89;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;62;-2193.619,-147.0161;Float;True;Property;_Distortion;Distortion;0;0;Create;True;0;0;False;0;None;f53512d44b91e954dae7bf028209df1a;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;63;-1906.417,-111.4026;Float;False;True;True;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;69;-1889.797,-21.13458;Float;False;Property;_distotfact;distotfact;2;0;Create;True;0;0;False;0;0,0;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-1695.223,-71.6476;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SinTimeNode;88;-1697.389,61.27915;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;58;-2160.224,-357.9378;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;60;-1858.463,-330.4625;Float;False;uv2;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;39;-1717.743,-219.3405;Float;False;60;0;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;89;-1553.159,-69.83872;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;64;-1379.072,-160.319;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;61;-1266.722,-77.35174;Float;True;Property;_Mask;Mask;1;0;Create;True;0;0;False;0;None;e8ac10704ef2e3f4aa6ec7286e64a6a4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;15;-2304.921,279.9458;Float;False;1463.679;871.44;Comment;14;3;10;12;14;13;7;5;11;6;16;17;18;19;20;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ScreenColorNode;4;-1228.531,-288.7747;Float;False;Global;_GrabScreen0;Grab Screen 0;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;13;-1145.83,368.1251;Float;False;dfif;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1627.201,658.4363;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;59;-1976.743,-495.0237;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;30;-1547.947,-455.623;Float;False;uv;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;14;-1806.324,653.0194;Float;False;13;0;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;6;-1387.069,321.6056;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1590.608,756.3186;Fixed;False;Constant;_Float2;Float 2;0;0;Create;True;0;0;False;0;0.8;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-1592.423,499.3005;Fixed;False;Constant;_Float0;Float 0;0;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;67;-982.5773,-320.7591;Float;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ComponentMaskNode;7;-1648.046,317.6162;Float;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.AbsOpNode;16;-1472.201,670.4363;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;3;-1842.891,417.8748;Half;False;Constant;_Vectorpt;Vectorpt;0;0;Create;True;0;0;False;0;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;18;-1794.201,733.4363;Fixed;False;Constant;_Float1;Float 1;0;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;11;-1438.332,368.9742;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;12;-1305.354,408.9636;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComputeScreenPosHlpNode;5;-1997.644,312.2162;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PowerNode;19;-1267.333,655.4753;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;52;-675.3273,-403.1395;Float;False;True;2;Float;ASEMaterialInspector;0;5;ASE/Shakewave;0b6a9f8b4f707c74ca64c0be8e590de0;0;0;SubShader 0 Pass 0;2;True;2;SrcAlpha;OneMinusSrcAlpha;0;One;Zero;False;True;Off;True;True;True;True;False;False;True;2;False;False;True;4;Queue=Transparent;IgnoreProjector=True;RenderType=Transparent;PreviewType=Plane;False;0;0;0;False;False;False;False;False;False;False;False;False;True;2;0;0;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;63;0;62;0
WireConnection;68;0;63;0
WireConnection;68;1;69;0
WireConnection;60;0;58;0
WireConnection;89;0;68;0
WireConnection;89;1;88;4
WireConnection;64;0;39;0
WireConnection;64;1;89;0
WireConnection;4;0;64;0
WireConnection;13;0;12;0
WireConnection;17;0;18;0
WireConnection;17;1;14;0
WireConnection;30;0;59;0
WireConnection;6;0;11;0
WireConnection;67;0;4;0
WireConnection;67;3;61;4
WireConnection;7;0;5;0
WireConnection;16;0;17;0
WireConnection;11;0;7;0
WireConnection;11;1;3;0
WireConnection;12;0;6;0
WireConnection;12;1;10;0
WireConnection;19;0;16;0
WireConnection;19;1;20;0
WireConnection;52;0;67;0
ASEEND*/
//CHKSM=B341463999573657D64834D3F4EC7B30DA2DCB5F