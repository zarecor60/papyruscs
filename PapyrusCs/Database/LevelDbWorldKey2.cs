﻿using System;

namespace PapyrusCs.Database
{
    public struct LevelDbWorldKey2
    {
        public bool Equals(LevelDbWorldKey2 other)
        {
            return KeyType == other.KeyType && X == other.X && Z == other.Z && SubChunkId == other.SubChunkId;
        }

        public override bool Equals(object obj)
        {
            return obj is LevelDbWorldKey2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = KeyType.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) X;
                hashCode = (hashCode * 397) ^ (int) Z;
                hashCode = (hashCode * 397) ^ SubChunkId.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(LevelDbWorldKey2 left, LevelDbWorldKey2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LevelDbWorldKey2 left, LevelDbWorldKey2 right)
        {
            return !left.Equals(right);
        }

        public LevelDbWorldKey2(byte[] key)
        {
            X = (key[0] | (key[1] << 8) | (key[2] << 16) | (key[3] << 24));
            Z = (key[4] | (key[5] << 8) | (key[6] << 16) | (key[7] << 24));
            KeyType = key[8];
            SubChunkId = key[9];
        }

        public byte KeyType { get; }
        public Int32 X { get; }
        public Int32 Z { get; }
        public byte SubChunkId { get; }

        private int GetGroup(int coord, int chunkPerDimension)
        {
            if (coord >= 0)
                return coord / chunkPerDimension;
            return ((coord + 1) / chunkPerDimension) - 1;
        }

        public UInt64 GetXZGroup(int chunkPerDimension)
        {
            UInt64 result = ((UInt32) GetGroup(this.X, chunkPerDimension)) << 32;
            result |= (UInt32) GetGroup(this.Z, chunkPerDimension);

            return result;
        }
    }
}