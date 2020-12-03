﻿namespace Project1.BusinessLibrary
{
    /// <summary>
    /// storeinventory class. Contains storeinventory fields and constructor
    /// </summary>
    public class StoreInventory
    {
        public int StoreInventoryLocationId { get; }
        public int StoreInventoryProductId { get; }
        public int StoreInventoryProductQty { get; }

        public StoreInventory(StoreLocation storeId)
        {
            StoreInventoryLocationId = storeId.StoreLocationId;
        }
    }
}
