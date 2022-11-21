namespace BubblesDivePlanner.Models.Cylinders
{
    public class Cylinder : ICylinder
    {
        public Cylinder(string name, ushort cylinderVolume, ushort cylinderPressure, byte surfaceAirConsumptionRate, IGasMixture gasMixture, ushort remainingGas, ushort usedGas)
        {
            Name = name;
            CylinderVolume = AssignCylinderVolume(cylinderVolume);
            CylinderPressure = AssignCylinderPressure(cylinderPressure);
            InitialPressurisedVolume = CalculateInitialPressurisedVolume();
            RemainingGas = remainingGas;
            RemainingGas = AssignRemainingGas(remainingGas);
            UsedGas = usedGas;
            SurfaceAirConsumptionRate = AssignSurfaceAirConsumptionRate(surfaceAirConsumptionRate);
            GasMixture = gasMixture;
        }

        public string Name { get; }
        public ushort CylinderVolume { get; }
        public ushort CylinderPressure { get; }
        public ushort InitialPressurisedVolume { get; private set; }
        public ushort RemainingGas { get; private set; }
        public ushort UsedGas { get; private set; }
        public byte SurfaceAirConsumptionRate { get; }
        public IGasMixture GasMixture { get; }

        public void UpdateCylinderGasConsumption(IDiveStep diveStep)
        {
            UsedGas = (ushort)(((diveStep.Depth / 10) + 1) * diveStep.Time * SurfaceAirConsumptionRate);
            RemainingGas = UsedGas < RemainingGas ? (ushort)(RemainingGas - UsedGas) : (ushort)0;
        }

        private ushort CalculateInitialPressurisedVolume()
        {
            return (ushort)(CylinderVolume * CylinderPressure);
        }

        private static ushort AssignCylinderVolume(ushort cylinderVolume) => cylinderVolume switch
        {
            > 30 => 30,
            < 3 => 3,
            _ => cylinderVolume
        };

        private static ushort AssignCylinderPressure(ushort cylinderPressure) => cylinderPressure switch
        {
            > 300 => 300,
            < 50 => 50,
            _ => cylinderPressure,
        };

        private static byte AssignSurfaceAirConsumptionRate(byte surfaceAirConsumptionRate) => surfaceAirConsumptionRate switch
        {
            > 30 => 30,
            < 3 => 3,
            _ => surfaceAirConsumptionRate
        };

        private ushort AssignRemainingGas(ushort remainingGas)
        {
            return remainingGas switch
            {
                0 => InitialPressurisedVolume,
                _ => remainingGas,
            };
        }
    }
}