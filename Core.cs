using System.Reflection;
using MelonLoader;
using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

[assembly: MelonInfo(typeof(RPCLogger), "RPCLogger", "1.0.0", "MasterHell")]

public class RPCLogger : MelonMod
{
    public override void OnInitializeMelon() => PatchMethodsWithAttribute();

    private void PatchMethodsWithAttribute()
    {
        var harmony = new HarmonyLib.Harmony("RPCLogger");

        List<(Type classType, string methodName)> methodsToHook =
        [
            (typeof(AI), "ApplyDamage"),
            (typeof(AI), "headless"),
            (typeof(AI), "RPCCortaDistancia"),
            (typeof(AI), "RPCLargaDistancia"),
            (typeof(AI), "RPCMedianaDistancia"),
            (typeof(AI), "Rushmode"),
            (typeof(AI), "SpeedChange"),
            (typeof(AI), "Stop"),
            (typeof(AI), "Stunned"),
            (typeof(AI), "TirarArma"),
            (typeof(AI), "TP"),
            (typeof(AI), "Transformacion"),
            (typeof(ArcaBotAI), "ApplyDamage"),
            (typeof(CardEasterEgg), "playsound"),
            (typeof(Chat), "EnviarMensaje"),
            (typeof(CollectAI), "Albino"),
            (typeof(CollectAI), "RPCCortaDistancia"),
            (typeof(CollectAI), "RPCLargaDistancia"),
            (typeof(CollectAI), "RPCMedianaDistancia"),
            (typeof(CollectAI), "Rushmode"),
            (typeof(CollectAI), "SearchCustards"),
            (typeof(CollectAI), "SpeedChange"),
            (typeof(CollectAI), "TirarArma"),
            (typeof(CollectAI), "TP"),
            (typeof(DaytimeChanger), "ChangeDTime"),
            (typeof(GulperAction), "gulp"),
            (typeof(InRoomChat), "Chat"),
            (typeof(ManualPhotonViewAllocator), "InstantiateRpc"),
            (typeof(MonstruoOnline), "Atacar"),
            (typeof(MonstruoOnline), "AtacarMoviendo"),
            (typeof(MonstruoOnline), "RPCCortaDistancia"),
            (typeof(MonstruoOnline), "RPCLargaDistancia"),
            (typeof(MonstruoOnline), "RPCMedianaDistancia"),
            (typeof(MonstruoOnline), "TirarArma"),
            (typeof(MonstruoOnline), "TP"),
            (typeof(MonstruoOnline), "Transformacion"),
            (typeof(MonstruoVersus), "ApplyDamage"),
            (typeof(MonstruoVersus), "Stunned"),
            (typeof(MSRSummoner), "playeff"),
            (typeof(OnClickDestroy), "DestroyRpc"),
            (typeof(PickupItem), "PunPickup"),
            (typeof(PickupItemSimple), "PunPickupSimple"),
            (typeof(PickupItemSyncer), "PickupItemInit"),
            (typeof(PickupItemSyncer), "RequestForPickupItems"),
            (typeof(PickupItemSyncer), "RequestForPickupTimes"),
            (typeof(PickupScript), "DeathStatus"),
            (typeof(PickupScript), "request"),
            (typeof(PickupScript), "SetStatus"),
            //(typeof(RoomPXC), "EnviarMensaje"),
            (typeof(SincronizacionPlayer), "addItem"),
            (typeof(SincronizacionPlayer), "cambiarModelo"),
            (typeof(SincronizacionPlayer), "disparo"),
            (typeof(SincronizacionPlayer), "escopetazo"),
            (typeof(SincronizacionPlayer), "ExplosionPl"),
            (typeof(SincronizacionPlayer), "LinternaON"),
            (typeof(SincronizacionPlayer), "meleeAnim"),
            (typeof(SincronizacionPlayer), "nontrailshot"),
            (typeof(SincronizacionPlayer), "proyectil"),
            (typeof(SincronizacionPlayer), "Reload"),
            (typeof(SincronizacionPlayer), "setDefault"),
            (typeof(SpeakerSpawn), "showUp"),
            (typeof(UnityStandardAssets.Characters.FirstPerson.FirstPersonController), "ApplyDamage"),
            (typeof(UnityStandardAssets.Characters.FirstPerson.FirstPersonController), "Emote"),
            (typeof(UnityStandardAssets.Characters.FirstPerson.FirstPersonController), "Medkit"),
            (typeof(UnityStandardAssets.Characters.FirstPerson.FirstPersonController), "Stunned")
        ];

        foreach (var (type, methodName) in methodsToHook)
        {
            var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            if (method == null)
            {
                MelonLogger.Warning($"Method not found: {type.FullName}.{methodName}");
                continue;
            }

            var prefix = typeof(RPCLogger).GetMethod(nameof(LogPrefix), BindingFlags.Static | BindingFlags.NonPublic);
            harmony.Patch(method, new HarmonyMethod(prefix));
            MelonLogger.Msg($"[Hooked] {type.FullName}.{methodName}");
        }
    }

    private static bool LogPrefix(MethodBase __originalMethod)
    {
        MelonLogger.Msg($"[IBKACJCMLNL] Called: {__originalMethod.DeclaringType.FullName}.{__originalMethod.Name}");
        return false;
    }
}

[HarmonyPatch(typeof(Il2Cpp.GBOJALILJAL), "MCPDAAPFEHL", [
    typeof(Il2Cpp.PhotonView),
    typeof(string),
    typeof(Il2Cpp.PhotonTargets),
    typeof(bool),
    typeof(Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<Il2CppSystem.Object>)
])]
public static class InternalRPCPatchTargets
{
    [HarmonyPrefix]
    private static void Prefix(Il2Cpp.PhotonView CJBCKJHFNAL, string GJHIKFADOJP,
        Il2Cpp.PhotonTargets LFAPAMEJOAB, bool MGENKJMBBFI,
        Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<Il2CppSystem.Object> KJMKAICJBJE)
    {
        if (GJHIKFADOJP != "EnviarMensaje")
            MelonLogger.Msg($"[InternalRPC] Method: {GJHIKFADOJP}, Target: {LFAPAMEJOAB}, View: {CJBCKJHFNAL?.name}");
    }
}

[HarmonyPatch(typeof(Il2Cpp.GBOJALILJAL), "INPELMNGAGK", [
    typeof(byte),
    typeof(Il2CppSystem.Object),
    typeof(bool),
    typeof(Il2Cpp.KKEBGFDDMCE)
])]
public static class RaiseEventPatch
{
    [HarmonyPrefix]
    private static void Prefix(byte MINDFMPIFLH, Il2CppSystem.Object HKLOKIFLNPN,
        bool LLCCMDKAALB, Il2Cpp.KKEBGFDDMCE JFKBOLMHFJB)
    {
        MelonLogger.Msg($"[RaiseEvent] Code: {MINDFMPIFLH}, Reliable: {LLCCMDKAALB}");
    }
}

[HarmonyPatch(typeof(Il2Cpp.PhotonView), "RPC", [
    typeof(string),
    typeof(Il2Cpp.JMAGIAOBBGI),
    typeof(Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<Il2CppSystem.Object>)
])]
public static class RPCPatchPlayer
{
    [HarmonyPrefix]
    private static void Prefix(string GJHIKFADOJP, Il2Cpp.JMAGIAOBBGI HAECNPPINDI,
        Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<Il2CppSystem.Object> KJMKAICJBJE)
    {
        MelonLogger.Msg($"[RPC] Method: {GJHIKFADOJP}, Player: {HAECNPPINDI?.AFKLCICOINH}");
    }
}

[HarmonyPatch(typeof(Il2Cpp.PhotonView), "RPC", [
    typeof(string),
    typeof(Il2Cpp.PhotonTargets),
    typeof(Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<Il2CppSystem.Object>)
])]
public static class RPCPatchTargets
{
    [HarmonyPrefix]
    private static void Prefix(string GJHIKFADOJP, Il2Cpp.PhotonTargets LFAPAMEJOAB,
        Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<Il2CppSystem.Object> KJMKAICJBJE)
    {
        if (GJHIKFADOJP != "EnviarMensaje")
            MelonLogger.Msg($"[RPC] Method: {GJHIKFADOJP}, Targets: {LFAPAMEJOAB}");
    }
}