﻿#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float4x4 WorldViewProjection;
float4x4 World;
float4x4 InverseTransposeWorld;

float3 ambientColor; // Light's Ambient Color
float3 diffuseColor; // Light's Diffuse Color
float3 specularColor; // Light's Specular Color
float KAmbient; 
float KDiffuse; 
float KSpecular;
float shininess; 
float3 lightPosition;
float3 eyePosition; // Camera position

float4x4 LightViewProjection;
float2 shadowMapSize;

static const float modulatedEpsilon = 0.000041200182749889791011810302734375;
static const float maxEpsilon = 0.000023200045689009130001068115234375;

texture shadowMap;
sampler2D shadowMapSampler =
sampler_state
{
	Texture = <shadowMap>;
	MinFilter = Point;
	MagFilter = Point;
	MipFilter = Point;
	AddressU = Clamp;
	AddressV = Clamp;
};

struct DepthPassVertexShaderInput
{
	float4 Position : POSITION0;
};

struct DepthPassVertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 ScreenSpacePosition : TEXCOORD1;
};
texture baseTexture;
sampler2D textureSampler = sampler_state
{
    Texture = (baseTexture);
    MagFilter = Linear;
    MinFilter = Linear;
    AddressU = Clamp;
    AddressV = Clamp;
};

struct VertexShaderInput
{
	float4 Position : POSITION0;
    float4 Normal : NORMAL;
    float2 TextureCoordinates : TEXCOORD0;
   	float4 LightSpacePosition : TEXCOORD2;

};

struct VertexShaderOutput
{
    float4 LightSpacePosition : TEXCOORD2;
	float4 Position : SV_POSITION;
    float2 TextureCoordinates : TEXCOORD0;
    float4 WorldPosition : TEXCOORD1;
    float4 Normal : TEXCOORD3;    
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;

    output.Position = mul(input.Position, WorldViewProjection);
    output.WorldPosition = mul(input.Position, World);
    output.Normal = mul(input.Normal, InverseTransposeWorld);
    output.TextureCoordinates = input.TextureCoordinates;
	output.LightSpacePosition = mul(output.Position, LightViewProjection);
  
	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    //Calculos Shadow
     float3 lightSpacePosition = input.LightSpacePosition.xyz / input.LightSpacePosition.w;
    float2 shadowMapTextureCoordinates = 0.5 * lightSpacePosition.xy + float2(0.5, 0.5);
    shadowMapTextureCoordinates.y = 1.0f - shadowMapTextureCoordinates.y;
	
    float3 normal = normalize(input.Normal.rgb);
    float3 lightDirection = normalize(lightPosition - input.WorldPosition.xyz);
    float inclinationBias = max(modulatedEpsilon * (1.0 - dot(normal, lightDirection)), maxEpsilon);
	
	// Sample and smooth the shadowmap
	// Also perform the comparison inside the loop and average the result
    float notInShadow = 0.0;
    float2 texelSize = 1.0 / shadowMapSize;
    for (int x = -1; x <= 1; x++)
        for (int y = -1; y <= 1; y++)
        {
            float pcfDepth = tex2D(shadowMapSampler, shadowMapTextureCoordinates + float2(x, y) * texelSize).r + inclinationBias;
            notInShadow += step(lightSpacePosition.z, pcfDepth) / 9.0;
        }
	

    //
    // Base vectors
    float3 viewDirection = normalize(eyePosition - input.WorldPosition.xyz);
    float3 halfVector = normalize(lightDirection + viewDirection);

	// Get the texture texel
    float4 texelColor = tex2D(textureSampler, input.TextureCoordinates);
    
	// Calculate the diffuse light
    float NdotL = saturate(dot(input.Normal.xyz, lightDirection));
    float3 diffuseLight = KDiffuse * diffuseColor * NdotL;

	// Calculate the specular light
    float NdotH = dot(input.Normal.xyz, halfVector);
    float3 specularLight = sign(NdotL) * KSpecular * specularColor * pow(saturate(NdotH), shininess);
    
    // Final calculation
    float4 finalColor = float4(saturate(ambientColor * KAmbient + diffuseLight) * texelColor.rgb + specularLight, texelColor.a);
    finalColor.rgb *= 0.7 + 0.3 * notInShadow;
    return finalColor;
}


VertexShaderOutput MainPlainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;

    output.Position = mul(input.Position, WorldViewProjection);
    output.WorldPosition = mul(input.Position, World);
    output.Normal = mul(input.Normal, InverseTransposeWorld);
	output.LightSpacePosition = mul(output.Position, LightViewProjection);
	
	return output;
}

float4 MainPlainPS(VertexShaderOutput input) : COLOR
{
     float3 lightSpacePosition = input.LightSpacePosition.xyz / input.LightSpacePosition.w;
    float2 shadowMapTextureCoordinates = 0.5 * lightSpacePosition.xy + float2(0.5, 0.5);
    shadowMapTextureCoordinates.y = 1.0f - shadowMapTextureCoordinates.y;
	
    float3 normal = normalize(input.Normal.rgb);
    float3 lightDirection = normalize(lightPosition - input.WorldPosition.xyz);
    float inclinationBias = max(modulatedEpsilon * (1.0 - dot(normal, lightDirection)), maxEpsilon);
	
	// Sample and smooth the shadowmap
	// Also perform the comparison inside the loop and average the result
    float notInShadow = 0.0;
    float2 texelSize = 1.0 / shadowMapSize;
    for (int x = -1; x <= 1; x++)
        for (int y = -1; y <= 1; y++)
        {
            float pcfDepth = tex2D(shadowMapSampler, shadowMapTextureCoordinates + float2(x, y) * texelSize).r + inclinationBias;
            notInShadow += step(lightSpacePosition.z, pcfDepth) / 9.0;
        }
	
  
    // Base vectors
    float3 viewDirection = normalize(eyePosition - input.WorldPosition.xyz);
    float3 halfVector = normalize(lightDirection + viewDirection);

	// Get the texture texel
   // float4 texelColor = tex2D(textureSampler, input.TextureCoordinates);
    
	// Calculate the diffuse light
    float NdotL = saturate(dot(input.Normal.xyz, lightDirection));
    float3 diffuseLight = KDiffuse * diffuseColor * NdotL;

	// Calculate the specular light
    float NdotH = dot(input.Normal.xyz, halfVector);
    float3 specularLight = sign(NdotL) * KSpecular * specularColor * pow(saturate(NdotH), shininess);
    
    // Final calculation
    float4 finalColor = float4(saturate(ambientColor * KAmbient + diffuseLight) + specularLight,1.0);
    finalColor.rgb *= 0.7 + 0.3 * notInShadow;
    return finalColor;
}

DepthPassVertexShaderOutput DepthVS(in DepthPassVertexShaderInput input)
{
	DepthPassVertexShaderOutput output;
	output.Position = mul(input.Position, WorldViewProjection);
	output.ScreenSpacePosition = mul(input.Position, WorldViewProjection);
	return output;
}

float4 DepthPS(in DepthPassVertexShaderOutput input) : COLOR
{
    float depth = input.ScreenSpacePosition.z / input.ScreenSpacePosition.w;
    return float4(depth, depth, depth, 1.0);
}


technique DepthPass
{
	pass Pass0
	{
		VertexShader = compile VS_SHADERMODEL DepthVS();
		PixelShader = compile PS_SHADERMODEL DepthPS();
	}
};
technique BasicColorDrawing
{
	pass Pass0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};
technique BasicPlainDrawing
{
	pass Pass0
	{
		VertexShader = compile VS_SHADERMODEL MainPlainVS();
		PixelShader = compile PS_SHADERMODEL MainPlainPS();
	}
};
