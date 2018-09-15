using System;

namespace Yow.Application.Exceptions {
    public class RecordNotFoundException : Exception {
        public RecordNotFoundException (string name, object key) : base ($"Could not find record for Entity '{name}' with Id of {key}") {
        }
        public RecordNotFoundException (string message) : base (message) {
        }
    }
}