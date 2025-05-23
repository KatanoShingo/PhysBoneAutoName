float4 edge = float4(0.0, 0.0, 0.0, 0.0);

inline float2 rotation(float2 p, float a)
{
    float s = sin(a);
    float c = cos(a);
    return float2(p.x*c - p.y*s, p.x*s + p.y*c);
}

inline float lPolygon(float2 p, uint n)
{
    float a = atan2(p.x,p.y)+UNITY_PI;
    float r = UNITY_PI*2./float(n);
    return cos(floor(.5+a/r)*r-a)*length(p);
}

inline float lStarPolygon(float2 p, uint n, float o)
{
    return (lPolygon(p,n) - lPolygon(rotation(p, UNITY_PI/float(n)), n) *o)/(1.-o);
}

inline float2 culc_pos(float2 uv, float2 pos2, float2 scale, float angle)
{
    float2 pos = (frac(uv)*2 - 1);
    pos -= pos2;
    pos = rotation(pos, angle*UNITY_PI);
    pos /= scale;
    return pos;
}

inline float culc_edge(float2 pos, float edgeWidth, float clipEdgeBlur , uint clipShape, float asteroidShape, uint clipNShape)
{
    float clipMask = 0;
    float edge = 0;
    // 円
    if (clipShape == 0)
        {
            clipMask = length(pos);
        }
        // 四角形
        else if (clipShape == 1)
        {
            clipMask = max(abs(pos.x), abs(pos.y));
        }
        // ひし形
        else if (clipShape == 2)
        {
            clipMask = abs(pos.x) + abs(pos.y);
        }
        // ハート
        else if (clipShape == 3)
        {
            clipMask = pow(pos.x, 2) + pow(pos.y - sqrt(abs(pos.x)), 2);
        }
        // アステロイド
        else if (clipShape == 4)
        {
            clipMask = pow( pow(abs(pos.x), asteroidShape) + pow(abs(pos.y), asteroidShape) , 1);
        }
        // n星
        else if (clipShape == 5)
        {
            clipMask = lStarPolygon(pos, clipNShape, 0.5);
        }

    clip(clipMask-1);

    if (clipEdgeBlur == 1)
        {
            edge = step(((edgeWidth)*clipMask), 1);
        }
    else
        {
            edge = smoothstep(1, clipEdgeBlur, edgeWidth*clipMask);
        }
    
    return edge;
}