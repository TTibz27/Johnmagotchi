#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler s0;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 color = tex2D(s0, input.TextureCoordinates);
	
    float avgColor = (color.r + color.g + color.b)/3;
	// Using 40% color 60% 'grayscale' 
    //color.rb = color.g;
    color.r = color.r * 0.7;
    color.g = color.g * 0.7;
    color.b = color.b * 0.7;
    color.r = color.r * 0.8 + avgColor * 0.2;
    color.g = color.g * 0.8 + avgColor * 0.2;
    color.b = color.b * 0.8 + avgColor * 0.2;
	
    return color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};