using System.ServiceModel;

namespace VariableIrony.ServiceModel {
    public static class ICommunicationObjectExtensions {
        public static void SafeDispose(this ICommunicationObject target) {
            if (null == target)
                return;

            if (target.State == CommunicationState.Faulted) {
                target.Abort();
            } else {
                target.Close();
            }
        }
    }
}
