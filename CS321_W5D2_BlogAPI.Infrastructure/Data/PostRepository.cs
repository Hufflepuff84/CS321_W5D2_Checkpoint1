﻿using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _dbContext;
        private Post post;

        public PostRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public Post Get(int id)
        {
            // TODO: Implement Get(id). Include related Blog and Blog.User
            // throw new NotImplementedException();
            return _dbContext.Posts.Include(x => x.Blog)
                .ThenInclude(y => y.User)
            .FirstOrDefault(x => x.Id == id);
            
            
        }

        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            // TODO: Implement GetBlogPosts, return all posts for given blog id
            // TODO: Include related Blog and AppUser
            // throw new NotImplementedException();
            return _dbContext.Posts.Include(p => p.Blog)
            .ThenInclude(b => b.User)
            .Where(b => b.BlogId == blogId);
        }

        public Post Add(Post Post)
        {
            // TODO: add Post
            // throw new NotImplementedException();
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
            return Post;
            
        }

        public Post Update(Post updatedPost)
        {
            // TODO: update Post
            // throw new NotImplementedException();
            var currentPost = this.Get(updatedPost.Id);
            if (currentPost == null) return null;
            _dbContext.Entry(currentPost).CurrentValues
                .SetValues(updatedPost);
            _dbContext.Posts.Update(currentPost);
            _dbContext.SaveChanges();
            return currentPost;
        }

        public IEnumerable<Post> GetAll()
        {
            // TODO: get all posts
            // throw new NotImplementedException();
            return _dbContext.Posts.Include(p => p.Blog)
                .ThenInclude(b => b.User);
        }

        public void Remove(int id)
        {
            // TODO: remove Post
            // throw new NotImplementedException();
            var currentPost = this.Get(id);
            if (currentPost != null)
            {
                _dbContext.Posts.Remove(currentPost);
                _dbContext.SaveChanges();
            }
        }

    }
}
