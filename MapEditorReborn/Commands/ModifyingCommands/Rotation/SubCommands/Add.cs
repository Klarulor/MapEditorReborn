﻿namespace MapEditorReborn.Commands.Rotation.SubCommands
{
    using System;
    using API.Extensions;
    using API.Features.Components;
    using API.Features.Components.ObjectComponents;
    using CommandSystem;
    using Events.Handlers.Internal;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using global::MapEditorReborn.Events.EventArgs;
    using UnityEngine;

    using static API.API;

    /// <summary>
    /// Modifies object's rotation by adding a certain value to the current one.
    /// </summary>
    public class Add : ICommand
    {
        /// <inheritdoc/>
        public string Command => "add";

        /// <inheritdoc/>
        public string[] Aliases => Array.Empty<string>();

        /// <inheritdoc/>
        public string Description => string.Empty;

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("mpr.rotation"))
            {
                response = $"You don't have permission to execute this command. Required permission: mpr.rotation";
                return false;
            }

            Player player = Player.Get(sender);
            if (!player.TryGetSessionVariable(SelectedObjectSessionVarName, out MapEditorObject mapObject) || mapObject == null)
            {
                if (!ToolGunHandler.TryGetMapObject(player, out mapObject))
                {
                    response = "You haven't selected any object!";
                    return false;
                }
                else
                {
                    ToolGunHandler.SelectObject(player, mapObject);
                }
            }

            if (mapObject is RoomLightComponent || mapObject is PlayerSpawnPointComponent)
            {
                response = "You can't modify this object's rotation!";
                return false;
            }

            if (arguments.Count >= 3 && float.TryParse(arguments.At(0), out float x) && float.TryParse(arguments.At(1), out float y) && float.TryParse(arguments.At(2), out float z))
            {
                Quaternion newRotation = Quaternion.Euler(x, y, z);

                ChangingObjectRotationEventArgs ev = new ChangingObjectRotationEventArgs(player, mapObject, newRotation.eulerAngles, true);
                Events.Handlers.MapEditorObject.OnChangingObjectRotation(ev);

                if (!ev.IsAllowed)
                {
                    response = ev.Response;
                    return true;
                }

                mapObject.transform.rotation *= Quaternion.Euler(ev.Rotation);
                player.ShowGameObjectHint(mapObject);

                mapObject.UpdateObject();

                response = ev.Rotation.ToString("F3");
                return true;
            }

            response = "Invalid values.";
            return false;
        }
    }
}
