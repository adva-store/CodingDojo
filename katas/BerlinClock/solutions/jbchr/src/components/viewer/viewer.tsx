import { OrbitControls } from "@react-three/drei";
import { Canvas } from "@react-three/fiber";
import { FC } from "react";
import Clock from "../clock/clock";
import styles from "./viewer.module.css";

interface ViewerProps {}

/**
 * A canvas containing controls, lights and the berlin clock
 */
const Viewer: FC<ViewerProps> = () => {
  return (
    <Canvas
      className={styles.viewer}
      camera={{ fov: 35, position: [0, 10, 25] }}
    >
      <ambientLight />
      <pointLight position={[10, 10, 10]} />

      <Clock />
      <OrbitControls></OrbitControls>
    </Canvas>
  );
};

export default Viewer;
