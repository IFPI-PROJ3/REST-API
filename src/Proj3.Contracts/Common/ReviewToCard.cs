namespace Proj3.Contracts.Common;

public record ReviewToCard
    (        
        Guid event_id,
        Guid volunteer_id,
        string volunteer_name,
        string volunteer_image,
        string review,
        float rating,
        DateTime created_at
    );
   

