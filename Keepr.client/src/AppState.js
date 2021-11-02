import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  accounts:[],
  keeps: [],
  vaults: [],
  keep: [],
  vault: '',
  vaultkeeps: '',
  vaultkeep: '',
  profile: null,
  profiles: [],
  profileVaults: [],
  accountVaults: [],
  profileVault: '',
  profileKeeps: [],
  profileKeep: '',
  vault_keep: []
})
