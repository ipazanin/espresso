import useDidMount from './useDidMount';
import useWillUnmount from './useWillUnmount';

type UseFullLifecycleHook = (
  onMount: () => void,
  onUnmount: () => void
) => void;

const useFullLifecyle: UseFullLifecycleHook = (onMount, onUnmount) => {
  useDidMount(onMount);

  useWillUnmount(onUnmount);
};

export default useFullLifecyle;
