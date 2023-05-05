using System.Text.RegularExpressions;
using EliteMMO.API;

namespace Mister
{
    #region class: PlayerInfo
    public class PlayerInfo
    {
        EliteAPI api;
        public PlayerInfo(EliteAPI _api)
        {
            api = _api;
        }
        float lastCastPercent = 1;
        public bool Casting
        {
            get
            {
                if (api.CastBar.Percent == 1)
                {
                    return false;
                }
                if (api.CastBar.Percent != lastCastPercent)
                {
                    lastCastPercent = api.CastBar.Percent;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public int Status => (int)api.Entity.GetLocalPlayer().Status;
        public string Name => api.Party.GetPartyMembers().First().Name;
        public int HPP => api.Party.GetPartyMembers().First().CurrentHPP;
        public int MPP => api.Party.GetPartyMembers().First().CurrentMPP;
        public uint HP => api.Party.GetPartyMembers().First().CurrentHP;
        public uint MP => api.Party.GetPartyMembers().First().CurrentMP;
        public uint MaxHP => api.Player.HPMax;
        public uint MaxMP => api.Player.MPMax;
        public int TP => (int)api.Party.GetPartyMembers().First().CurrentTP;
        public int MainJob => api.Player.GetPlayerInfo().MainJob;
        public int MainJobLevel => api.Player.GetPlayerInfo().MainJobLevel;
        public int SubJob => api.Player.GetPlayerInfo().SubJob;
        public int SubJobLevel => api.Player.GetPlayerInfo().SubJobLevel;
        public bool HasBuff(short id) => api.Player.GetPlayerInfo().Buffs.Any(b => b == id);
        public int HasBuffcount(short id) => api.Player.GetPlayerInfo().Buffs.Where(b => b == id).Count();
        public bool HasAbility(uint id) => api.Player.HasAbility(id);
        public bool HasAbility(string name)
        {
            return api.Player.HasAbility(api.Resources.GetAbility(name, 0).ID);
        }

        public bool HasSpell(uint id) => api.Player.HasSpell(id);
        public bool HasSpell(string name)
        {
            return api.Player.HasSpell(api.Resources.GetSpell(name, 0).Index);
        }
        public bool HasBlueMagicSpellSet(int id) => api.Player.HasBlueMagicSpellSet(id);
        public bool HasWeaponSkill(uint id) => api.Player.HasWeaponSkill(id);
        public int ServerID => (int)api.Entity.GetLocalPlayer().ServerID;
        public int TargetID => (int)api.Entity.GetLocalPlayer().TargetID;
        public float X => api.Entity.GetLocalPlayer().X;
        public float Y => api.Entity.GetLocalPlayer().Y;
        public float Z => api.Entity.GetLocalPlayer().Z;
        public float H => api.Entity.GetLocalPlayer().H;
        public bool HasKeyItem(uint id) => api.Player.HasKeyItem(id);
        public int MeritPoints => api.Player.GetPlayerInfo().MeritPoints;
        public int UsedJobPoints => api.Player.GetJobPoints(MainJob).SpentJobPoints;
        public int UseableJobPoints => api.Player.GetJobPoints(MainJob).JobPoints;
        public int CapacityPoints => api.Player.GetJobPoints(MainJob).CapacityPoints;
        public double GetAngleFrom(double x, double z)
        {
            var angleInDegrees = (Math.Atan2(Z - z,
                X - x) * 180 / Math.PI) * -1;
            return (Math.Floor(angleInDegrees * (10 ^ 0) + 0.5) / (10 ^ 0));
        }
        public bool DynaZone()
        {
            List<int> DynaZones = new List<int>(new int[] { 39, 40, 41, 42, 134, 135, 185, 186, 187, 188 });
            if (DynaZones.Contains(api.Player.ZoneId)) return true;
            else return false;
        }
        public string DynaTime()
        {
            string vtz = "Morning";
            uint vanahour = api.VanaTime.CurrentHour;
            if (vanahour >= 0 && vanahour < 8) vtz = "Morning";
            else if (vanahour >= 8 && vanahour < 16) vtz = "Noon";
            else if (vanahour >= 16 && vanahour < 24) vtz = "Night";
            return vtz;
        }
        //public bool DynaStrike(string typ, string time, string target)
        //{
        //    if (!PlayerInfo.DynaZone()) return true;
        //    if (target == "Nightmare Taurus") return true;
        //    else if (NoProcDynaMobs.Contains(target)) return NoneProc;
        //    else if (DynaMobProc[time][typ].Contains(target)) return true;
        //    return false;
        //}
        public int GetFinishingMoves()
        {
            var retVal = 0;
            if (HasBuff(381)) { retVal = 1; }
            else if (HasBuff(382)) { retVal = 2; }
            else if (HasBuff(383)) { retVal = 3; }
            else if (HasBuff(384)) { retVal = 4; }
            else if (HasBuff(385)) { retVal = 5; }
            else if (HasBuff(588)) { retVal = 6; }
            return retVal;
        }
    }
    #endregion
    #region class: TargetInfo
    public class TargetInfo
    {
        EliteAPI api;
        public TargetInfo(EliteAPI _api)
        {
            api = _api;
        }

        public void Attack()
        {
            var count = 0;

            while (new PlayerInfo(api).Status == 0)
            {
                new WindowInfo(api).SendText("/attack <t>");
                Thread.Sleep(TimeSpan.FromSeconds(3.0));

                if (new PlayerInfo(api).Status == 1 || count == 2)
                    break;

                count = count + 1;
                Thread.Sleep(TimeSpan.FromSeconds(1.0));
            }
        }
        public string Name => api.Entity.GetEntity((int)api.Target.GetTargetInfo().TargetIndex).Name;
        public int ID => (int)api.Entity.GetEntity((int)api.Target.GetTargetInfo().TargetIndex).TargetID;
        public int HPP => api.Entity.GetEntity((int)api.Target.GetTargetInfo().TargetIndex).HealthPercent;
        public double Distance => Math.Truncate((10 * api.Entity.GetEntity((int)api.Target.GetTargetInfo().TargetIndex).Distance) / 10);
        public bool LockedOn => api.Target.GetTargetInfo().LockedOn;
        public float X => api.Entity.GetEntity((int)api.Target.GetTargetInfo().TargetIndex).X;
        public float Y => api.Entity.GetEntity((int)api.Target.GetTargetInfo().TargetIndex).Y;
        public float Z => api.Entity.GetEntity((int)api.Target.GetTargetInfo().TargetIndex).Z;
        public float H => api.Entity.GetEntity((int)api.Target.GetTargetInfo().TargetIndex).H;
        public void SetTarget(int ID) => api.Target.SetTarget(ID);
        public int GetTargetIdByName(string name)
        {
            for (var x = 0; x < 2048; x++)
            {
                var ID = api.Entity.GetEntity(x);

                if (ID.Name != null && ID.Name.ToLower().Equals(name.ToLower()))
                    return (int)ID.TargetID;
            }
            return -1;
        }
        public int GetTargetIndexByName(string name)
        {
            for (var x = 0; x < 2048; x++)
            {
                var ID = api.Entity.GetEntity(x);

                if (ID.Name != null && ID.Name.ToLower().Equals(name.ToLower()))
                    return (int)ID.ServerID;
            }
            return -1;
        }
        public void FaceTarget(float x, float z)
        {
            if (ID == new PlayerInfo(api).ServerID || ID == 0)
                return;

            var p = api.Entity.GetLocalPlayer();
            var angle = (byte)(Math.Atan((z - p.Z) / (x - p.X)) * -(128.0f / Math.PI));

            if (p.X > x)
                angle += 128;

            var radian = (((float)angle) / 255) * 2 * Math.PI;
            api.Entity.SetEntityHPosition(api.Entity.LocalPlayerIndex, (float)radian);
        }
        public bool IsFacingTarget(float x1, float z1, float h1, float x2, float z2)
        {
            var angle = GetDifferenceAngle(x1, z1, x2, z2);
            var rotation = ((h1 / (2 * 3.14159265359f)) * 255);
            return Math.Abs((angle - rotation) + -128.0f) < 20;
        }
        private float GetDifferenceAngle(float x1, float z1, float x2, float z2)
        {
            var angle = Math.Atan((z2 - z1) / (x2 - x1));
            angle *= -(128.0f / 3.14159265359f);
            return (float)(x2 > x1 ? angle + 128 : angle);
        }
    }
    #endregion
    #region class: RecastInfo
    public class Recast
    {
        EliteAPI api;
        public Recast(EliteAPI _api)
        {
            api = _api;
        }

        public int GetSpellRecast(int id) => api.Recast.GetSpellRecast(id);
        public int GetSpellRecast(string name) => api.Recast.GetSpellRecast(api.Resources.GetSpell(name, 0).Index);
        public int GetAbilityRecast(int id)
        {
            var IDs = api.Recast.GetAbilityIds();
            for (var x = 0; x < IDs.Count; x++)
            {
                if (IDs[x] == id)
                    return api.Recast.GetAbilityRecast(x);
            }
            return 0;
        }
        public int GetAbilityRecast(string name)
        {
            var IDs = api.Recast.GetAbilityIds();
            for (var x = 0; x < IDs.Count; x++)
            {
                if (IDs[x] == (int)api.Resources.GetAbility(name, 0).TimerID)
                    return api.Recast.GetAbilityRecast(x);
            }
            return 0;
        }

    }
    #endregion
    #region class: WindowInfo
    public class WindowInfo
    {
        EliteAPI api;
        public WindowInfo(EliteAPI _api)
        {
            api = _api;
        }

        public void SendText(string text) => api.ThirdParty.SendString(text);
        public void KeyPress(EliteMMO.API.Keys key) => api.ThirdParty.KeyPress(key);
        public void KeyUp(EliteMMO.API.Keys key) => api.ThirdParty.KeyUp(key);
        public void KeyDown(EliteMMO.API.Keys key) => api.ThirdParty.KeyDown(key);
    }
    #endregion
    #region class: PartyInfo
    public class PartyInfo
    {
        EliteAPI api;
        public PartyInfo(EliteAPI _api)
        {
            api = _api;
        }

        public int Count(int PartyNumber = 0)
        {
            var allience = api.Party.GetAllianceInfo();
            var pc = 0;
            if (PartyNumber == 1 || PartyNumber == 0)
                pc = pc + allience.Party0Count;
            if (PartyNumber == 2 || PartyNumber == 0)
                pc = pc + allience.Party1Count;
            if (PartyNumber == 3 || PartyNumber == 0)
                pc = pc + allience.Party2Count;
            return pc;
        }
        public bool ContainsName(string name)
        {
            foreach (var member in api.Party.GetPartyMembers().Where(p => p.Active != 0).ToList())
            {
                if (Regex.Replace(member.Name, "([A-Z])", " $1", RegexOptions.Compiled).Trim() == name)
                    return true;
            }
            return false;
        }

        public bool ContainsID(uint ID)
        {
            foreach (var member in api.Party.GetPartyMembers().Where(p => p.Active != 0).ToList())
            {
                if (member.ID == ID)
                    return true;
            }
            return false;
        }
        public int averageHPP()
        {
            int hpp = 0;
            var members = api.Party.GetPartyMembers().Where(p => p.Active != 0).ToList();
            foreach (var member in members)
            {
                if (member.Active != 0)
                    hpp = (hpp + member.CurrentHPP);
            }
            return (int)Math.Round((double)(hpp / members.Count));
        }
        public bool lowMP(uint val)
        {
            List<EliteAPI.PartyMember> members = api.Party.GetPartyMembers().Where(p => p.Active != 0 && p.Name == "Kupipi").ToList();
            foreach (EliteAPI.PartyMember member in members)
            {
                if (member.Name != new PlayerInfo(api).Name && member.CurrentMPP < val)
                {
                    return true;
                }
            }
            return false;

        }
    }
    #endregion
    #region class: PetInfo
    public class PetInfo
    {
        EliteAPI api;
        public PetInfo(EliteAPI _api)
        {
            api = _api;
        }

        public string Name
        {
            get
            {
                var p = api.Entity.GetEntity(api.Entity.GetLocalPlayer().PetIndex).Name;
                return p;
                // return p != null
                //     ? Regex.Replace(api.Entity.GetEntity(api.Entity.GetLocalPlayer().PetIndex).Name, "([A-Z])", " $1",
                //         RegexOptions.Compiled).Trim() : null;
            }
        }
        public int ID => (int)api.Entity.GetEntity(api.Entity.GetLocalPlayer().PetIndex).ServerID;
        public int HPP => api.Entity.GetEntity(api.Entity.GetLocalPlayer().PetIndex).HealthPercent;
        public int MPP => api.Entity.GetEntity(api.Entity.GetLocalPlayer().PetIndex).ManaPercent;
        public int TPP => (int)api.Player.PetTP;
        public int Status => (int)api.Entity.GetEntity(api.Entity.GetLocalPlayer().PetIndex).Status;
    }
    #endregion
    #region class: Inventory
    public class Inventory
    {
        EliteAPI api;
        public Inventory(EliteAPI _api)
        {
            api = _api;
        }

        public int ItemQuantityByName(string name)
        {
            var count = api.Inventory.GetContainerCount(0);
            var itemc = 0;

            for (var x = 0; x < count; x++)
            {
                var item = api.Inventory.GetContainerItem(0, x);
                if (item.Id != 0 && api.Resources.GetItem(item.Id).Name[0] == name)
                {
                    itemc = itemc + (int)item.Count;
                }
            }
            return itemc;
        }
    }
    #endregion
    #region class: Movement
    public class Movement
    {
        EliteAPI api;
        public Movement(EliteAPI _api)
        {
            api = _api;
        }

        float lastX;
        float lastY;
        float lastZ;
        public bool Moving = false;
        public bool Stuck = false;
        public bool HomeStuck = false;

        public void Move(EliteAPI.XiEntity Target)
        {
            if (Target == null) return;
            if (Moving)
            {
                float dX = lastX - api.Player.X;
                float dY = lastY - api.Player.Y;
                float dZ = lastZ - api.Player.Z;
                double distance = Math.Sqrt(dX * dX + dY * dY + dZ * dZ);
                if (distance < .1) Stuck = true;
            }

            float tX = Target.X - api.Player.X;
            float tY = Target.Y - api.Player.Y;
            float tZ = Target.Z - api.Player.Z;

            api.AutoFollow.SetAutoFollowCoords(tX, tY, tZ);
            api.AutoFollow.IsAutoFollowing = true;

            lastX = api.Player.X;
            lastY = api.Player.Y;
            lastZ = api.Player.Z;
            HomeStuck = false;
            Moving = true;
        }
        public void Stop()
        {
            Moving = false;
            api.AutoFollow.IsAutoFollowing = false;
        }

        public void Move(float X, float Y, float Z)
        {
            float tX = X - api.Player.X;
            float tY = Y - api.Player.Y;
            float tZ = Z - api.Player.Z;

            api.AutoFollow.SetAutoFollowCoords(tX, tY, tZ);
            api.AutoFollow.IsAutoFollowing = true;

            lastX = api.Player.X;
            lastY = api.Player.Y;
            lastZ = api.Player.Z;
        }
    }

    #endregion

}