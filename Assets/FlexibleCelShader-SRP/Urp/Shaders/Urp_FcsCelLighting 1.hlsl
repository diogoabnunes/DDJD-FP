//
void FcsCelLighting1_float(
        float3 WorldNormal,
        float3 WorldPosition,
        float3 GlobalColor,
        float3 TexMapSample,
        float3 NormMapSample,
        float3 EmissMapSample,
		float3 EmissColorScalar,
        float  MaxRampLevels,
        float  LightScalar,
        float3 HighColor,
        float3 LowColor,
        out float3 Color
    )
{
	half3 lightDirection;
	half shadowAtten;

#ifdef SHADERGRAPH_PREVIEW
	lightDirection = half3(0.5, 0.5, 0);
	shadowAtten = 1;
#else
#ifdef SHADOWS_SCREEN
	half4 clipPos = TransformWorldToHClip(WorldPosition);
	half4 shadowCoord = ComputeScreenPos(clipPos);
#else
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPosition);
#endif
	Light mainLight = GetMainLight(shadowCoord);
	lightDirection = mainLight.direction;
	shadowAtten = mainLight.shadowAttenuation;
#endif


	Color = TexMapSample;

	// Calculate light intensity
	half intensity = dot(WorldNormal, lightDirection);
	intensity = clamp(intensity * LightScalar, 0, 1);
	//intensity *= shadowAtten;// clamp(intensity - (1 - shadowAtten), 0, 1);

	// Determine Level based on Intensity
	MaxRampLevels = MaxRampLevels - 1;
	half level = ceil((intensity * MaxRampLevels));

	// Calculate light Color
	half3 highCol = (level / MaxRampLevels) * HighColor;
	half3 lowCol = ((MaxRampLevels - level) / MaxRampLevels) * LowColor;
	Color *= (highCol + lowCol) / 2;

	// Apply Emissive Light
	EmissMapSample *= EmissColorScalar;
	half eIntensity = max(EmissMapSample.r, EmissMapSample.g);
	eIntensity = max(eIntensity, EmissMapSample.b);
	Color = EmissMapSample * eIntensity + Color * (1 - eIntensity);

	Color *= GlobalColor;
}