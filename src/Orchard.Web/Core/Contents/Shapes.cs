﻿using Orchard.ContentManagement;
using Orchard.DisplayManagement.Descriptors;

namespace Orchard.Core.Contents {
    public class Shapes : IShapeTableProvider {
        public void Discover(ShapeTableBuilder builder) {
            builder.Describe("Content")
                .Configure(descriptor => descriptor.Wrappers.Add("Content_ControlWrapper"))
                .OnCreated(created => {
                    var content = created.Shape;
                    content.Main.Add(created.New.PlaceChildContent(Source: content));
                })
                .OnDisplaying(displaying => {
                    ContentItem contentItem = displaying.Shape.ContentItem;
                    if (contentItem != null) {
                        //Content-BlogPost
                        displaying.ShapeMetadata.Alternates.Add("Content__" + contentItem.ContentType);
                        //Content-42
                        displaying.ShapeMetadata.Alternates.Add("Content__" + contentItem.Id);
                        //Content.Summary
                        displaying.ShapeMetadata.Alternates.Add("Content_" + displaying.ShapeMetadata.DisplayType);
                        //Content.Summary-Page
                        displaying.ShapeMetadata.Alternates.Add("Content_" + displaying.ShapeMetadata.DisplayType + "__" + contentItem.ContentType);
                    }
                });

            builder.Describe("Content_Edit")
               .OnDisplaying(displaying => {
                   ContentItem contentItem = displaying.Shape.ContentItem;
                   if (contentItem != null) {
                       //Content.Editor-Page
                       displaying.ShapeMetadata.Alternates.Add("Content_Edit__" + contentItem.ContentType);
                   }
               });
        }
    }
}
