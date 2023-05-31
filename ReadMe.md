## BluePrint Content Architect

### Introduction

- Items
  1. AB Test 
     - for develop flow
  2. Cloud Mini Game 
     - for Event
  3. Command Batching 
     - for battle reward
  4. Daily Reward
     - for daily reward
  5. GameLogic On The Server Side
     - for anti-cheat
  6. Mail-Box
     - for event
  7. Loot Boxes with Cool Down
     - for business
  8. Rewarded Ads Unity Mediation
     - for business
  9. Seasonal Event
     - for business
  10. Starter Pack
      - for business
  11. Virtual Shop
      - for business
  12. Battle Pass
       - for business

- Model
    1. ~~ClientDataModel -> AES~~
       1. ~~RowData -> string (json)~~
          * ~~FirstColumnValue -> string~~
          * ~~Headers -> List:string~~
          * ~~Values -> List:string~~
       2. ~~Message -> class~~
          * ~~tick -> long~~
          * ~~log -> string~~
          * ~~success -> bool~~
          
    2. ~~CloudDataModel -> AES~~
       1. ~~RowData -> string (json)~~
          * ~~FirstColumnValue -> string~~
          * ~~Headers -> List:string~~
          * ~~Values -> List:string~~
       2. ~~Message -> class~~
           * ~~tick -> long~~
           * ~~log -> string~~
           * ~~success -> bool~~
       3. ~~ID -> class~~
           * ~~projectID -> string~~
           * ~~playerID -> string~~
           * ~~accessToken -> string~~
          
    3. CommandBatching Model
       1. Battle
          * List<Command> : class
          * CallProcessBatchEndpoint -> params : string[]
    4. ~~AlarmData -> AES~~
       1. ~~ID -> class~~
          * ~~projectID -> string~~
          * ~~playerID -> string~~
          * ~~accessToken -> string~~
       2. ~~Message -> class~~
          * ~~tick -> long~~
          * ~~log -> string~~
          * ~~success -> bool~~
    5. ~~SecurityInfo -> struct~~
          * ~~hmac -> byte[]~~
          * ~~iv -> byte[]~~
          * ~~key -> byte[]~~
          * ~~encryptedData -> byte[]~~

    6. ~~Tags~~
        1. ~~Save & Load~~
           * ~~Player Item, Abilities, Pets, etc.~~
           * ~~Player Progress~~
           * ~~Player Achievements~~
           * ~~Player Settings & Normal Data~~
           * ~~Player Social Info (Friends, Mail, Clan etc.)~~
           * ~~Player Virtual Currency & Premium Inventory~~
        2. ~~Send Something~~
           * ~~Messages or Chat~~
           * ~~Feedbacks~~
        3. ~~Battle~~
           * ~~Request Batching Commands~~
           * ~~Request Reward~~
           * ~~Request Validation~~
      
- Model Container
    1. Units
       1. NPC
       2. Monster
       3. Pets
    2. Skills
       1. Effect
       2. All Names
    3. Items
       1. Consumable
       2. Equipment
       3. Material
       4. Currency
       5. Premium Items
          - Compositions
       6. Premium Packages
          - Compositions
    4. Level Data
       1. Dungeon Monsters
    5. Quests
       1. Daily
       2. Weekly
       3. Monthly
       4. Event
       5. Achievement
    