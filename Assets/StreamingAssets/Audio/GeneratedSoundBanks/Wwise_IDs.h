/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID BUTTON_GENERIC = 1301445487U;
        static const AkUniqueID MUSIC_FOREST = 2181728358U;
        static const AkUniqueID MUSIC_MENU = 1598298728U;
        static const AkUniqueID NAV_BACKWARD = 647488260U;
        static const AkUniqueID NAV_FORWARD = 3621467206U;
        static const AkUniqueID PAUSE = 3092587493U;
        static const AkUniqueID PLAYER_JUMP = 1305133589U;
        static const AkUniqueID SLIME_JUMP = 2006797914U;
        static const AkUniqueID SLIME_LAND = 2608073725U;
        static const AkUniqueID START_MODE = 3412652423U;
        static const AkUniqueID UNPAUSE = 3412868374U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAME_PAUSED
        {
            static const AkUniqueID GROUP = 3367563548U;

            namespace STATE
            {
                static const AkUniqueID PAUSED = 319258907U;
                static const AkUniqueID UNPAUSED = 1365518790U;
            } // namespace STATE
        } // namespace GAME_PAUSED

    } // namespace STATES

    namespace SWITCHES
    {
        namespace MATERIAL
        {
            static const AkUniqueID GROUP = 3865314626U;

            namespace SWITCH
            {
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID STONE = 1216965916U;
            } // namespace SWITCH
        } // namespace MATERIAL

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID ENVIRONMENT_VOLUME = 2054307141U;
        static const AkUniqueID MASTER_VOLUME = 4179668880U;
        static const AkUniqueID MUSIC_VOLUME = 1006694123U;
        static const AkUniqueID SFX_VOLUME = 1564184899U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID ENVIRONMENT = 1229948536U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SFX = 393239870U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
