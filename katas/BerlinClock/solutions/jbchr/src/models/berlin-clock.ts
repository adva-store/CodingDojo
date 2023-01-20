import { Vector3 } from "@react-three/fiber";

export type BerlinClockTime = [number, number, number, number, number];

export interface Column {
  position: Vector3;
  width: number;
}
