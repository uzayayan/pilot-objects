namespace Space.PilotObjects.Camera.Composition
{
    public static class CompositionFactory
    {
        public static IComposition Create(ECompositionTypes compositionType)
        {
            switch (compositionType)
            {
                case ECompositionTypes.GRID_01:
                    return new TwoGridComposition();
                case ECompositionTypes.GRID_02:
                    return new ThirdGridComposition();
                case ECompositionTypes.CROSSHAIR:
                    return new CrosshairComposition();
                case ECompositionTypes.RABATMENT:
                    return new RabatmentComposition();
            }

            return null;
        }
    }
}