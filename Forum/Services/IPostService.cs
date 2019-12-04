﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Models;
using Forum.Services.Communication;

namespace Forum.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetAsync(string id);
        Task<IEnumerable<Post>> GetOrderByVoteAsync();
        Task<IEnumerable<Post>> GetOrderByDateAsync();
        Task<PostResponse> AddAsync(Post post);
        Task<PostResponse> UpdateAsync(string id, Post post);
        Task<PostResponse> RemoveAsync(string id);
        Task<VoteResponse> Vote(string PostId, string UserId);
        Task<VoteResponse> UnVote(string PostId, string UserId);
        Task<bool> UserOwnsPostAsync(string PostId, string UserId);
    }
}