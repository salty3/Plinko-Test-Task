namespace Tools.Runtime
{
    public static class MathTool
    {
        public static float RemapRange(float source, float sourceFrom, float sourceTo, float targetFrom, float targetTo)
        {
            return targetFrom + (source-sourceFrom)*(targetTo-targetFrom)/(sourceTo-sourceFrom);
        }
    }
}