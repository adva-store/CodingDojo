import { Vector3 } from "@react-three/fiber";
import React, { FC } from "react";

interface ClockBoxProps {
  width: number;
  position: Vector3;
  color: number;
}

/**
 * Displays one box of the berlin clock's 4 lower rows
 */
const ClockBox: FC<ClockBoxProps> = React.memo(function ClockBox(props) {
  return (
    <mesh position={props.position}>
      <boxGeometry attach="geometry" args={[props.width, 1, 1]} />
      <meshStandardMaterial color={props.color} />
    </mesh>
  );
});

export default ClockBox;
