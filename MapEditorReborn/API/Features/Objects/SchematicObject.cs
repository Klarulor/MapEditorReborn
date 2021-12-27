﻿namespace MapEditorReborn.API.Features.Objects
{
    using System;
    using Exiled.API.Enums;
    using UnityEngine;

    /// <summary>
    /// A tool used to spawn and save schematics.
    /// </summary>
    [Serializable]
    public class SchematicObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SchematicObject"/> class.
        /// </summary>
        public SchematicObject()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchematicObject"/> class.
        /// </summary>
        /// <param name="schematicName">The schematic's name.</param>
        public SchematicObject(string schematicName) => SchematicName = schematicName;

        /// <summary>
        /// Gets or sets the <see cref="SchematicObject"/>'s name.
        /// </summary>
        public string SchematicName { get; set; } = "None";

        /// <summary>
        /// Gets or sets the <see cref="SchematicObject"/>'s position.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SchematicObject"/>'s rotation.
        /// </summary>
        public Vector3 Rotation { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SchematicObject"/>' scale.
        /// </summary>
        public Vector3 Scale { get; set; } = Vector3.one;

        /// <summary>
        /// Gets or sets the <see cref="Exiled.API.Enums.RoomType"/> which is used to determine the spawn position and rotation of the <see cref="SchematicObject"/>.
        /// </summary>
        public RoomType RoomType { get; set; }
    }
}
