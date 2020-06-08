﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Models;

namespace Forum.Contracts.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string ImageId { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<PostForUserResponse> myPosts { get; set; }
        public virtual ICollection<ThreadResponse> myThread { get; set; }
    }
}