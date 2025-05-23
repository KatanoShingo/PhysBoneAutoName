float _TargetPosX;
float _TargetPosY;
float _TargetPosZ;
float4 _HeartColor;
float _HeartStrength;
float _DistanceThreshold;
sampler2D _HeartTex;
sampler2D _MaskTex;

float3 lilCustomColor(float3 col, float2 uv, float3 positionWS)
{
    float3 target = float3(_TargetPosX, _TargetPosY, _TargetPosZ);
    float dist = distance(positionWS, target);
    float fade = saturate(1.0 - dist / _DistanceThreshold);

    float mask = tex2D(_MaskTex, uv).r;
    float3 heartCol = tex2D(_HeartTex, uv).rgb * _HeartColor.rgb;

    return lerp(col, heartCol, fade * mask * _HeartStrength);
}
