// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ASE/WaterRelect"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_Water("Water", 2D) = "white" {}
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
				#pragma target 2.0
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
					
				};
				
				uniform sampler2D _MainTex;
				uniform fixed4 _TintColor;
				uniform float4 _MainTex_ST;
				uniform sampler2D_float _CameraDepthTexture;
				uniform float _InvFade;
				uniform sampler2D _GrabTexture;
				uniform sampler2D _Water;
				uniform float4 _Water_ST;

				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					

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

					float2 uv3 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float2 appendResult5 = (float2(uv3.x , ( 1.0 - uv3.y )));
					float2 uv_Water = i.texcoord.xy * _Water_ST.xy + _Water_ST.zw;
					float mulTime29 = _Time.y * 0.1;
					float2 appendResult31 = (float2(( uv_Water.x + mulTime29 ) , uv_Water.y));
					float2 appendResult33 = (float2(tex2D( _Water, appendResult31 ).g , 0.0));
					float2 temp_output_10_0 = ( appendResult5 + ( appendResult33 * 0.03 ) );
					float2 appendResult40 = (float2((temp_output_10_0).x , (0.2 + ((temp_output_10_0).y - 0.0) * (1.0 - 0.2) / (1.0 - 0.0))));
					float4 screenColor2 = tex2D( _GrabTexture, appendResult40 );
					

					fixed4 col = ( _TintColor * screenColor2 );
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
121;722;1562;720;910.7874;-125.0434;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;32;-1713.211,567.3243;Float;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-1728.772,280.8725;Float;False;0;8;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;29;-1576.449,493.7884;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-1464.462,289.9347;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;31;-1320.462,377.9347;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;8;-1180.412,308.1251;Float;True;Property;_Water;Water;0;0;Create;True;0;0;False;0;None;5e254b7b0b83a5e419f402ca40b38341;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1350.63,21.26551;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;35;-826.4654,513.4587;Float;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;False;0;0.03;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;4;-1121.078,109.4367;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;33;-842.1895,343.1765;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-705.1895,306.1765;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;5;-925.4266,40.61207;Float;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;-689.3535,131.4773;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;37;-520.6168,272.1731;Float;False;False;True;False;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;36;-303.6168,247.1731;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.2;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;38;-503.6168,176.1731;Float;False;True;False;False;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;40;-449.6168,45.17313;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;6;-594.9155,-173.3244;Float;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;2;-265.8882,3.711716;Float;False;Global;_GrabScreen0;Grab Screen 0;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-219.499,-122.5269;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;5;ASE/WaterRelect;0b6a9f8b4f707c74ca64c0be8e590de0;0;0;SubShader 0 Pass 0;2;True;2;SrcAlpha;OneMinusSrcAlpha;0;One;Zero;False;True;Off;True;True;True;True;False;False;True;2;False;False;True;4;Queue=Transparent;IgnoreProjector=True;RenderType=Transparent;PreviewType=Plane;False;0;0;0;False;False;False;False;False;False;False;False;False;True;0;0;0;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;29;0;32;0
WireConnection;30;0;17;1
WireConnection;30;1;29;0
WireConnection;31;0;30;0
WireConnection;31;1;17;2
WireConnection;8;1;31;0
WireConnection;4;0;3;2
WireConnection;33;0;8;2
WireConnection;34;0;33;0
WireConnection;34;1;35;0
WireConnection;5;0;3;1
WireConnection;5;1;4;0
WireConnection;10;0;5;0
WireConnection;10;1;34;0
WireConnection;37;0;10;0
WireConnection;36;0;37;0
WireConnection;38;0;10;0
WireConnection;40;0;38;0
WireConnection;40;1;36;0
WireConnection;2;0;40;0
WireConnection;7;0;6;0
WireConnection;7;1;2;0
WireConnection;0;0;7;0
ASEEND*/
//CHKSM=4461C4CAB66AC688687D91DBFC816A6876055A8C