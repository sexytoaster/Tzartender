Shader "Unlit/Liquid"
{
    Properties
    {
        tint ("Tint", Color) = (1,1,1,1)
        mainTex ("Texture", 2D) = "white" {}
        fillAmount ("Fill Amount", Range(.43,.59)) = 0.0
		offsetColor ("Offset Top Colour", Color) = (1,1,1,1)
		[HideInInspector] topColor ("Top Color", Color) = (1,1,1,1)
        [HideInInspector] wobbleX ("WobbleX", Range(-1,1)) = 0.0
        [HideInInspector] wobbleZ ("WobbleZ", Range(-1,1)) = 0.0
        
    }
 
    SubShader
    {
        Tags {"Queue"="Geometry"  "DisableBatching" = "True" }
 
        Pass
        {
         Cull Off // we want all faces
         AlphaToMask On // transparency
 
         CGPROGRAM
 
 
         #pragma vertex vert
         #pragma fragment frag
         // make fog work
         #pragma multi_compile_fog
           
         #include "UnityCG.cginc"
 
         struct appdata
         {
           float4 vertex : POSITION;
           float2 uv : TEXCOORD0;
           float3 normal : NORMAL; 
         };
 
         struct v2f
         {
            float2 uv : TEXCOORD0;
            UNITY_FOG_COORDS(1)
            float4 vertex : SV_POSITION;
            float3 viewDir : COLOR;
            float3 normal : COLOR2;    
            float fillEdge : TEXCOORD2;
         };
 
         sampler2D mainTex;
         float4 mainTex_ST;
         float fillAmount, wobbleX, wobbleZ;
         float4 topColor,tint, offsetColor;
      
 
         v2f vert (appdata v)
         {
            v2f o;
 
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, mainTex);
            UNITY_TRANSFER_FOG(o,o.vertex);        
            // get world position of vert
            float3 worldPos = mul (unity_ObjectToWorld, v.vertex.xyz);  
            // rotate it around XY
            float3 worldPosX= worldPos.xyz;
            // rotate around XZ
            float3 worldPosZ = float3 (worldPosX.y, worldPosX.z, worldPosX.x);     
            //// cuse wobble script to define wobble of liquid
            float3 worldPosAdjusted = worldPos + (worldPosX  * wobbleX)+ (worldPosZ* wobbleZ);
            // what level liquid is at
            o.fillEdge =  worldPosAdjusted.y + fillAmount;
 
            o.viewDir = normalize(ObjSpaceViewDir(v.vertex));
            o.normal = v.normal;
            return o;
         }
           
         fixed4 frag (v2f i, fixed facing : VFACE) : SV_Target
         {
           // sample the texture
           fixed4 col = tex2D(mainTex, i.uv) * tint;
           // apply fog
           UNITY_APPLY_FOG(i.fogCoord, col);
          
           float4 result = step(i.fillEdge, 0.5);
           float4 resultColored = result * col;
 
           // color of back facing sides and top
           float4 topColor = (tint + offsetColor) * (result);
           return facing > 0 ? resultColored: topColor;
               
         }
         ENDCG
        }
 
    }
}