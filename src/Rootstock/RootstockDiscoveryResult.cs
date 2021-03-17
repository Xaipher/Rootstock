namespace Rootstock
{
    internal readonly struct RootstockDiscoveryResult
    {
        public RootstockDiscoveryResult(IRootstockInstallerMarker instance, RootstockGroup rootstockGroup, RootstockPriority localPriority, bool isValidInstaller)
        {
            Instance = instance;
            RootstockGroup = rootstockGroup;
            LocalPriority = localPriority;
            IsValidInstaller = isValidInstaller;
        }
        
        public readonly IRootstockInstallerMarker Instance;
        public readonly RootstockGroup RootstockGroup;
        public readonly RootstockPriority LocalPriority;
        public readonly bool IsValidInstaller;
    }
}