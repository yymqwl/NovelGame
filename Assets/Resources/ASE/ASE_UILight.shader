// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ASE/UILight"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
		_TextureSample2("Texture Sample 2", 2D) = "black" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		
		Pass
		{
			Name "Default"
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			
			#include "UnityShaderVariables.cginc"

			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				UNITY_VERTEX_OUTPUT_STEREO
				
			};
			
			uniform fixed4 _Color;
			uniform fixed4 _TextureSampleAdd;
			uniform float4 _ClipRect;
			uniform sampler2D _MainTex;
			uniform sampler2D _TextureSample2;
			uniform float4 _MainTex_ST;
			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID( IN );
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				OUT.worldPosition = IN.vertex;
				
				
				OUT.worldPosition.xyz +=  float3( 0, 0, 0 ) ;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
				OUT.color = IN.color * _Color;
				return OUT;
			}

			fixed4 frag(v2f IN  ) : SV_Target
			{
				float2 temp_cast_0 = (0.2795469).xx;
				float2 uv34 = IN.texcoord.xy * float2( 1,1 ) + float2( 0.5,0 );
				float2 panner35 = ( 1.0 * _Time.y * temp_cast_0 + uv34);
				float2 uv_MainTex = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				
				half4 color = ( ( float4( (tex2D( _TextureSample2, panner35 )).rgb , 0.0 ) + tex2D( _MainTex, uv_MainTex ) ) * _Color );
				
				color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
				
				#ifdef UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif

				return color;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
307;596;1562;669;1063.228;781.5545;1.388311;True;True
Node;AmplifyShaderEditor.CommentaryNode;37;-1966.362,-1155.462;Float;False;1573.264;642.9958;Comment;6;36;33;35;38;34;40;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;36;-1795.183,-886.7886;Float;False;Constant;_Vector1;Vector 1;3;0;Create;True;0;0;False;0;0.5,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;34;-1510.077,-1004.215;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;38;-1670.895,-691.4512;Float;False;Constant;_Float2;Float 2;3;0;Create;True;0;0;False;0;0.2795469;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;35;-1169.397,-941.9249;Float;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;22;-1122.341,-406.9597;Float;True;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;33;-841.0866,-923.0938;Float;True;Property;_TextureSample2;Texture Sample 2;1;0;Create;True;0;0;False;0;None;f0c751e3e6b99b549bfc463f96fc132f;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;40;-618.4754,-655.7254;Float;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;30;-748.6047,-418.533;Float;True;Property;_TextureSample1;Texture Sample 1;0;0;Create;True;0;0;False;0;None;fa3108da2fe38a748bfce58b4c9b5410;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;39;-105.4579,-645.4969;Float;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;14;-42.66482,-398.7096;Float;True;0;0;_Color;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;17;-1399.219,137.9798;Float;False;Constant;_Float0;Float 0;0;0;Create;True;0;0;False;0;1;0.8009292;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;28;-1402.453,316.7426;Float;False;Constant;_Vector0;Vector 0;2;0;Create;True;0;0;False;0;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;344.1506,-469.1802;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;23;-682.7531,-109.5738;Float;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;21;-518.825,159.8564;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;None;fa3108da2fe38a748bfce58b4c9b5410;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-1401.426,-178.6775;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;32;-747.446,358.6325;Float;False;Property;_Float1;Float 1;2;0;Create;True;0;0;False;0;1;0.184;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;27;-1076.367,210.6007;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;19;-982.6027,-104.9596;Float;True;Property;_ramp;ramp;3;1;[NoScaleOffset];Create;True;0;0;False;0;f0c751e3e6b99b549bfc463f96fc132f;f0c751e3e6b99b549bfc463f96fc132f;False;black;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;20;-720.6759,166.6803;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;654.0225,-421.505;Float;False;True;2;Float;ASEMaterialInspector;0;3;ASE/UILight;5056123faa0c79b47ab6ad7e8bf059a4;0;0;Default;2;True;2;SrcAlpha;OneMinusSrcAlpha;0;One;Zero;False;True;Off;False;False;True;2;False;False;True;5;Queue=Transparent;IgnoreProjector=True;RenderType=Transparent;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;0;0;False;False;False;False;False;False;False;False;False;True;2;0;0;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;34;1;36;0
WireConnection;35;0;34;0
WireConnection;35;2;38;0
WireConnection;33;1;35;0
WireConnection;40;0;33;0
WireConnection;30;0;22;0
WireConnection;39;0;40;0
WireConnection;39;1;30;0
WireConnection;16;0;39;0
WireConnection;16;1;14;0
WireConnection;0;0;16;0
ASEEND*/
//CHKSM=296DD89EC29319A3453A0724FEA711640EC915F6